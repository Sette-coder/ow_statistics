using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClient : MonoBehaviour
{
    private const string BASE_API_URL = "https://api.thaseven.com/owstatistics/api";

    //private const string BASE_API_URL = "http://localhost:5000/owstatistics/api";
    public static ApiClient Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // ─── Web Request Builders ──────────────────────────────
    private UnityWebRequest CreateGetWebRequest(string endpoint)
    {
        var request = new UnityWebRequest(BASE_API_URL + endpoint, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        AttachAuthToken(request);
        return request;
    }

    private UnityWebRequest CreatePostWebRequest(string body, string endpoint)
    {
        var request = new UnityWebRequest(BASE_API_URL + endpoint, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        AttachAuthToken(request);
        return request;
    }

    private UnityWebRequest CreatePutWebRequest(string body, string endpoint)
    {
        var request = new UnityWebRequest(BASE_API_URL + endpoint, "PUT");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        AttachAuthToken(request);
        return request;
    }

    private void AttachAuthToken(UnityWebRequest request)
    {
        if (UserDataManager.Instance != null && UserDataManager.Instance.IsLoggedIn)
            request.SetRequestHeader("Authorization", $"Bearer {UserDataManager.Instance.AccessToken}");
    }

    // ─── Token Refresh ─────────────────────────────────────

    // Tries to refresh the access token silently.
    // Returns true if successful, false if the session has fully expired.
    private async Task<bool> TryRefreshTokenAsync()
    {
        var refreshToken = UserDataManager.Instance?.RefreshToken;
        if (string.IsNullOrEmpty(refreshToken))
        {
            Debug.LogWarning("No refresh token available — forcing logout.");
            ForceLogout();
            return false;
        }

        var body = JsonConvert.SerializeObject(new RefreshRequest { RefreshToken = refreshToken });
        var request = CreatePostWebRequest(body, "/user/refresh");

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning($"Token refresh failed ({request.responseCode}) — forcing logout.");
            ForceLogout();
            return false;
        }

        var response = JsonConvert.DeserializeObject<RefreshResponse>(request.downloadHandler.text);
        UserDataManager.Instance.StoreTokens(response.AccessToken, response.RefreshToken);
        Debug.Log("Token refreshed silently.");
        return true;
    }

    // Retries a GET request once after a silent token refresh on 401
    private async Task<UnityWebRequest> SendWithRefreshAsync(
        UnityWebRequest request, Func<UnityWebRequest> rebuilder)
    {
        await request.SendWebRequest();

        if (request.responseCode != 401)
            return request;

        Debug.Log("401 received — attempting silent token refresh.");
        var refreshed = await TryRefreshTokenAsync();
        if (!refreshed) return request;

        // Rebuild the request with the new token and retry once
        var retry = rebuilder();
        await retry.SendWebRequest();
        return retry;
    }

    private void ForceLogout()
    {
        GameEventManager.Instance.OnLogout.Invoke(this, EventArgs.Empty);
    }

    // ─── Database Data ─────────────────────────────────────
    public async Task<(List<FromDatabaseMaps> Maps, List<FromDatabaseHeroes> Heroes)> GetDatabaseData()
    {
        var mapsRequest = await SendWithRefreshAsync(
            CreateGetWebRequest("/map/get-all-maps"),
            () => CreateGetWebRequest("/map/get-all-maps"));

        if (mapsRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR RETRIEVING MAPS: {mapsRequest.responseCode} - {mapsRequest.error}");
            return (null, null);
        }

        var maps = JsonConvert.DeserializeObject<List<FromDatabaseMaps>>(mapsRequest.downloadHandler.text);

        var heroesRequest = await SendWithRefreshAsync(
            CreateGetWebRequest("/hero/get-all-heroes"),
            () => CreateGetWebRequest("/hero/get-all-heroes"));

        if (heroesRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR RETRIEVING HEROES: {heroesRequest.responseCode} - {heroesRequest.error}");
            return (null, null);
        }

        var heroes = JsonConvert.DeserializeObject<List<FromDatabaseHeroes>>(heroesRequest.downloadHandler.text);

        Debug.Log($"Database data retrieved — Maps: {maps.Count} | Heroes: {heroes.Count}");
        return (maps, heroes);
    }

    // ─── User Management ───────────────────────────────────
    [ShowInInspector]
    public async Task<GenericResponse> CreateUserAsync(string username, string email, string password)
    {
        var body = JsonConvert.SerializeObject(new CreateUserRequest
        {
            Username = username, Email = email, Password = password, Role = "Client"
        });
        var request = CreatePostWebRequest(body, "/user/create");
        await request.SendWebRequest();

        var response = JsonConvert.DeserializeObject<GenericResponse>(request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR CREATING USER: {request.responseCode} - {response?.ResponseMessage}");
            response.ResponseMessage = $"Error {request.responseCode}: {response?.ResponseMessage}";
            return response;
        }

        Debug.Log($"User created successfully: {response.ResponseMessage}");
        return response;
    }

    [ShowInInspector]
    public async Task<LoginResponse> TryLogin(string usernameOrEmail, string password)
    {
        var body = JsonConvert.SerializeObject(new LoginRequest
                                                   { UsernameOrEmail = usernameOrEmail, Password = password });
        var request = CreatePostWebRequest(body, "/user/login");
        await request.SendWebRequest();

        var response = JsonConvert.DeserializeObject<LoginResponse>(request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR LOGIN: {request.responseCode} - {response?.LoginMessage}");
            return response;
        }

        Debug.Log($"Login successful — user: {response.Username}, role: {response.Role}");
        GameEventManager.Instance.OnLoginSuccess.Invoke(this, response);
        return response;
    }

    // ─── Match Management ──────────────────────────────────
    [ShowInInspector]
    public async Task<GenericResponse> SendMatchData(MatchDataSubmitRequest matchData)
    {
        var body = JsonConvert.SerializeObject(matchData);
        var request = await SendWithRefreshAsync(
            CreatePostWebRequest(body, "/match/create"),
            () => CreatePostWebRequest(body, "/match/create"));

        var response = JsonConvert.DeserializeObject<GenericResponse>(request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR SENDING MATCH: {request.responseCode} - {response?.ResponseMessage}");
            response.ResponseMessage = $"Error {request.responseCode}: {response?.ResponseMessage}";
            return response;
        }

        Debug.Log($"Match uploaded successfully: {response.ResponseMessage}");
        return response;
    }

    public async Task<List<MatchDto>> GetMatchListByUserId(int userId)
    {
        var body = JsonConvert.SerializeObject(new UserIdRequest { UserId = userId });
        var request = await SendWithRefreshAsync(
            CreatePostWebRequest(body, "/match/get-by-user-id"),
            () => CreatePostWebRequest(body, "/match/get-by-user-id"));

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR RETRIEVING MATCHES: {request.responseCode} - {request.error}");
            return null;
        }

        var response = JsonConvert.DeserializeObject<List<MatchDto>>(request.downloadHandler.text);
        Debug.Log($"Match list retrieved — count: {response?.Count}");
        return response;
    }

    public async Task<GenericResponse> UpdateMatchAsync(int matchId, UpdateMatchRequest matchData)
    {
        var body = JsonConvert.SerializeObject(matchData);
        var request = await SendWithRefreshAsync(
            CreatePutWebRequest(body, $"/match/update/{matchId}"),
            () => CreatePutWebRequest(body, $"/match/update/{matchId}"));

        var response = JsonConvert.DeserializeObject<GenericResponse>(request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR UPDATING MATCH: {request.responseCode} - {response?.ResponseMessage}");
            response.ResponseMessage = $"Error {request.responseCode}: {response?.ResponseMessage}";
            return response;
        }

        Debug.Log($"Match updated successfully: {response.ResponseMessage}");
        return response;
    }

    // ─── Admin Only ────────────────────────────────────────
    [ShowInInspector]
    private async Task<MapResponse> CreateMapAsync(string mapName, MapMode mapMode)
    {
        if (mapMode == MapMode.Default)
        {
            Debug.LogError("Default map mode not supported");
            return null;
        }

        var body = JsonConvert.SerializeObject(new CreateMapRequest
        {
            Name = mapName, Mode = mapMode.ToString(), ModeId = (int)mapMode
        });
        var request = await SendWithRefreshAsync(
            CreatePostWebRequest(body, "/map/create"),
            () => CreatePostWebRequest(body, "/map/create"));

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR CREATING MAP: {request.responseCode} - {request.error}");
            return null;
        }

        var response = JsonConvert.DeserializeObject<MapResponse>(request.downloadHandler.text);
        Debug.Log($"Map created: {response?.Name}");
        return response;
    }

    [ShowInInspector]
    private async Task<HeroResponse> CreateHeroAsync(string heroName, HeroRoles role)
    {
        if (role == HeroRoles.None)
        {
            Debug.LogError("None role not supported");
            return null;
        }

        var body = JsonConvert.SerializeObject(new CreateHeroRequest
                                                   { Name = heroName, Role = role.ToString() });
        var request = await SendWithRefreshAsync(
            CreatePostWebRequest(body, "/hero/create"),
            () => CreatePostWebRequest(body, "/hero/create"));

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR CREATING HERO: {request.responseCode} - {request.error}");
            return null;
        }

        var response = JsonConvert.DeserializeObject<HeroResponse>(request.downloadHandler.text);
        Debug.Log($"Hero created: {response?.Name}");
        return response;
    }
}