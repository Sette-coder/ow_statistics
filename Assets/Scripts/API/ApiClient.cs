using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Networking;
using Sirenix.OdinInspector;


public class ApiClient : MonoBehaviour
{
    public static ApiClient Instance;

    private void SingletonSetup()
    {
        if (Instance == null)
            Instance = this;
    }

    private readonly MapData[] _mapsList = new[]
    {
        new MapData("King's Row", MapMode.Hybrid),
        new MapData("Watchpoint: Gibraltar", MapMode.Escort),
        new MapData("Numbani", MapMode.Hybrid),
        new MapData("Dorado", MapMode.Escort),
        new MapData("Hollywood", MapMode.Hybrid),
        new MapData("Lijiang Tower", MapMode.Control),
        new MapData("Ilios", MapMode.Control),
        new MapData("Nepal", MapMode.Control),
        new MapData("Route 66", MapMode.Escort),
        new MapData("Eichenwalde", MapMode.Hybrid),
        new MapData("Oasis", MapMode.Control),
        new MapData("Junkertown", MapMode.Escort),
        new MapData("Blizzard World", MapMode.Hybrid),
        new MapData("Rialto", MapMode.Escort),
        new MapData("Busan", MapMode.Control),
        new MapData("Havana", MapMode.Escort),
        new MapData("New Queen Street", MapMode.Push),
        new MapData("Circuit Royal", MapMode.Escort),
        new MapData("Colosseo", MapMode.Push),
        new MapData("Midtown", MapMode.Hybrid),
        new MapData("Paraíso", MapMode.Hybrid),
        new MapData("Esperança", MapMode.Push),
        new MapData("Shambali Monastery", MapMode.Escort),
        new MapData("Antarctic Peninsula", MapMode.Control),
        new MapData("New Junk City", MapMode.Flashpoint),
        new MapData("Suravasa", MapMode.Flashpoint),
        new MapData("Samoa", MapMode.Control),
        new MapData("Runasapi", MapMode.Push),
        new MapData("Aatlis", MapMode.Flashpoint)
    };

    private readonly Hero[] _heroes = new[]
    {
        new Hero("Tracer", HeroRoles.Damage),
        new Hero("Reaper", HeroRoles.Damage),
        new Hero("Widowmaker", HeroRoles.Damage),
        new Hero("Phara", HeroRoles.Damage),
        new Hero("Reinhardt", HeroRoles.Tank),
        new Hero("Mercy", HeroRoles.Support),
        new Hero("Torbjörn", HeroRoles.Damage),
        new Hero("Hanzo", HeroRoles.Damage),
        new Hero("Winston", HeroRoles.Tank),
        new Hero("Zenyatta", HeroRoles.Support),
        new Hero("Bastion", HeroRoles.Damage),
        new Hero("Symmetra", HeroRoles.Damage),
        new Hero("Zarya", HeroRoles.Tank),
        new Hero("Cassidy", HeroRoles.Damage),
        new Hero("Soldier: 76", HeroRoles.Damage),
        new Hero("Lúcio", HeroRoles.Support),
        new Hero("Roadhog", HeroRoles.Tank),
        new Hero("Junkrat", HeroRoles.Damage),
        new Hero("D.Va", HeroRoles.Tank),
        new Hero("Mei", HeroRoles.Damage),
        new Hero("Genji", HeroRoles.Damage),
        new Hero("Ana", HeroRoles.Support),
        new Hero("Sombra", HeroRoles.Damage),
        new Hero("Orisa", HeroRoles.Tank),
        new Hero("Doomfist", HeroRoles.Tank),
        new Hero("Moira", HeroRoles.Support),
        new Hero("Brigitte", HeroRoles.Support),
        new Hero("Wrecking Ball", HeroRoles.Tank),
        new Hero("Ashe", HeroRoles.Damage),
        new Hero("Baptiste", HeroRoles.Support),
        new Hero("Sigma", HeroRoles.Tank),
        new Hero("Echo", HeroRoles.Damage),
        new Hero("Sojourn", HeroRoles.Damage),
        new Hero("Junker Queen", HeroRoles.Tank),
        new Hero("Kiriko", HeroRoles.Support),
        new Hero("Ramattra", HeroRoles.Tank),
        new Hero("Lifeweaver", HeroRoles.Support),
        new Hero("Illari", HeroRoles.Support),
        new Hero("Mauga", HeroRoles.Tank),
        new Hero("Venture", HeroRoles.Damage),
        new Hero("Juno", HeroRoles.Support),
        new Hero("Hazard", HeroRoles.Tank),
        new Hero("Freja", HeroRoles.Damage),
        new Hero("Wuyang", HeroRoles.Support),
        new Hero("Vendetta", HeroRoles.Damage),
        new Hero("Domina", HeroRoles.Tank),
        new Hero("Emre", HeroRoles.Damage),
        new Hero("Mizuki", HeroRoles.Support),
        new Hero("Anran", HeroRoles.Damage),
        new Hero("Jetpack Cat", HeroRoles.Support),
    };

    private string baseUrl = "https://api.thaseven.com/owstatistics/api"; // Your local API

    [ShowInInspector]
    private async void InitializeAllMaps()
    {
        foreach (MapData map in _mapsList)
        {
            try
            {
                await CreateMapAsync(map.MapName, map.Mode);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error creating map: {map.MapName} with error: {e.Message}");
            }
        }
    }

    [ShowInInspector]
    private async void InitializeAllHeroes()
    {
        foreach (Hero hero in _heroes)
        {
            try
            {
                await CreateHeroAsync(hero.HeroName, hero.Role);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error creating Hero: {hero.HeroName} with error: {e.Message}");
            }
        }
    }

    private void Awake()
    {
        SingletonSetup();
    }

    [ShowInInspector]
    public async Task<GenericResponse> CreateUserAsync(string username, string email, string password)
    {
        CreateUserRequest requestData = new CreateUserRequest
        {
            Username = username,
            Email = email,
            Password = password,
            Role = "Client"
        };

        string json = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/user/create", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        GenericResponse response = JsonUtility.FromJson<GenericResponse>(request.downloadHandler.text);
        if (request.result == UnityWebRequest.Result.Success)
        {
            return response;
        }
        else
        {
            Debug.LogError("Error creating user: " + request.error);
            response.ResponseMessage = $"Error {request.responseCode}, {response.ResponseMessage}";
            return response;
        }
    }

    [ShowInInspector]
    private async Task<MapResponse> CreateMapAsync(string mapName, MapMode mapMode)
    {
        if (mapMode == MapMode.Default)
        {
            Debug.LogError("Default map mode not supported");
            return null;
        }

        CreateMapRequest requestData = new CreateMapRequest
        {
            Name = mapName,
            Mode = mapMode.ToString(),
            ModeId = (int)mapMode
        };

        string json = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/map/create", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            MapResponse response = JsonUtility.FromJson<MapResponse>(request.downloadHandler.text);
            Debug.Log($"response: {response}");
            return response;
        }
        else
        {
            Debug.LogError("Error creating new Map: " + request.error);
            return null;
        }
    }

    [ShowInInspector]
    private async Task<HeroResponse> CreateHeroAsync(string heroName, HeroRoles role)
    {
        if (role == HeroRoles.None)
        {
            Debug.LogError("None role not supported");
            return null;
        }

        CreateHeroRequest requestData = new CreateHeroRequest
        {
            Name = heroName,
            Role = role.ToString(),
        };

        string json = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/hero/create", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            HeroResponse response = JsonUtility.FromJson<HeroResponse>(request.downloadHandler.text);
            Debug.Log($"response: {response.Name}");
            return response;
        }
        else
        {
            Debug.LogError("Error creating new Hero: " + request.error);
            return null;
        }
    }

    [ShowInInspector]
    public async Task<LoginResponse> TryLogin(string usernameOrEmail, string password)
    {
        LoginRequest requestData = new LoginRequest
        {
            UsernameOrEmail = usernameOrEmail,
            Password = password,
        };

        string json = JsonUtility.ToJson(requestData);
        UnityWebRequest request = new UnityWebRequest(baseUrl + "/user/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(
                $"LOGIN COMPLETED you are authorized: {response.Authorized}, with message: {response.LoginMessage}");
            GameEventManager.Instance.OnLoginSuccess.Invoke(this, response);
            return response;
        }
        else
        {
            Debug.LogError($"ERROR: {request.responseCode} with message: {response.LoginMessage}");
            return response;
        }
    }

    [ShowInInspector]
    public async Task<GenericResponse> SendMatchData(MatchDataSubmitRequest matchData)
    {
        string json = JsonUtility.ToJson(matchData);
        
        

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/match/create", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        GenericResponse response = JsonUtility.FromJson<GenericResponse>(request.downloadHandler.text);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Match Upload OK: {response.ok}, with message: {response.ResponseMessage}");
            return response;
        }
        else
        {
            Debug.LogError($"ERROR: {request.responseCode} with message: {response.ResponseMessage}");
            response.ResponseMessage = $"Error {request.responseCode}, {response.ResponseMessage}";
            return response;
        }
    }

    public async Task<List<MatchResponse>> GetMatchListByEmail(string userEmail)
    {
        EmailRequest requestData = new EmailRequest
        {
            UserEmail = userEmail
        };
        
        string json = JsonUtility.ToJson(requestData);
        UnityWebRequest request = new UnityWebRequest(baseUrl + "/match/get-by-email", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        MatchListResponse response = JsonUtility.FromJson<MatchListResponse>(request.downloadHandler.text);

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Match List retrieved Successfully: {response.Matches.Count}");
            return response.Matches;
        }
        else
        {
            Debug.LogError($"ERROR: {request.responseCode} with message: {request.error}");
            return null;
        }
    }
}