using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Sirenix.OdinInspector;
using Newtonsoft.Json;
using static Tha7.Utility.FromDatabaseWrapper;


public class ApiClient : MonoBehaviour
{
    public static ApiClient Instance;

    private void SingletonSetup()
    {
        if (Instance == null)
            Instance = this;
    }

    private readonly Map[] _mapsList = new[]
    {
        new Map { Name = "King's Row", Mode = MapMode.Hybrid },
        new Map { Name = "Watchpoint: Gibraltar", Mode = MapMode.Escort },
        new Map { Name = "Numbani", Mode = MapMode.Hybrid },
        new Map { Name = "Dorado", Mode = MapMode.Escort },
        new Map { Name = "Hollywood", Mode = MapMode.Hybrid },
        new Map { Name = "Lijiang Tower", Mode = MapMode.Control },
        new Map { Name = "Ilios", Mode = MapMode.Control },
        new Map { Name = "Nepal", Mode = MapMode.Control },
        new Map { Name = "Route 66", Mode = MapMode.Escort },
        new Map { Name = "Eichenwalde", Mode = MapMode.Hybrid },
        new Map { Name = "Oasis", Mode = MapMode.Control },
        new Map { Name = "Junkertown", Mode = MapMode.Escort },
        new Map { Name = "Blizzard World", Mode = MapMode.Hybrid },
        new Map { Name = "Rialto", Mode = MapMode.Escort },
        new Map { Name = "Busan", Mode = MapMode.Control },
        new Map { Name = "Havana", Mode = MapMode.Escort },
        new Map { Name = "New Queen Street", Mode = MapMode.Push },
        new Map { Name = "Circuit Royal", Mode = MapMode.Escort },
        new Map { Name = "Colosseo", Mode = MapMode.Push },
        new Map { Name = "Midtown", Mode = MapMode.Hybrid },
        new Map { Name = "Paraíso", Mode = MapMode.Hybrid },
        new Map { Name = "Esperança", Mode = MapMode.Push },
        new Map { Name = "Shambali Monastery", Mode = MapMode.Escort },
        new Map { Name = "Antarctic Peninsula", Mode = MapMode.Control },
        new Map { Name = "New Junk City", Mode = MapMode.Flashpoint },
        new Map { Name = "Suravasa", Mode = MapMode.Flashpoint },
        new Map { Name = "Samoa", Mode = MapMode.Control },
        new Map { Name = "Runasapi", Mode = MapMode.Push },
        new Map { Name = "Aatlis", Mode = MapMode.Flashpoint }
    };

    private readonly Hero[] _heroes = new[]
    {
        new Hero { Name = "Tracer", Role = HeroRoles.Damage },
        new Hero { Name = "Reaper", Role = HeroRoles.Damage },
        new Hero { Name = "Widowmaker", Role = HeroRoles.Damage },
        new Hero { Name = "Pharah", Role = HeroRoles.Damage },
        new Hero { Name = "Reinhardt", Role = HeroRoles.Tank },
        new Hero { Name = "Mercy", Role = HeroRoles.Support },
        new Hero { Name = "Torbjörn", Role = HeroRoles.Damage },
        new Hero { Name = "Hanzo", Role = HeroRoles.Damage },
        new Hero { Name = "Winston", Role = HeroRoles.Tank },
        new Hero { Name = "Zenyatta", Role = HeroRoles.Support },
        new Hero { Name = "Bastion", Role = HeroRoles.Damage },
        new Hero { Name = "Symmetra", Role = HeroRoles.Damage },
        new Hero { Name = "Zarya", Role = HeroRoles.Tank },
        new Hero { Name = "Cassidy", Role = HeroRoles.Damage },
        new Hero { Name = "Soldier: 76", Role = HeroRoles.Damage },
        new Hero { Name = "Lúcio", Role = HeroRoles.Support },
        new Hero { Name = "Roadhog", Role = HeroRoles.Tank },
        new Hero { Name = "Junkrat", Role = HeroRoles.Damage },
        new Hero { Name = "D.Va", Role = HeroRoles.Tank },
        new Hero { Name = "Mei", Role = HeroRoles.Damage },
        new Hero { Name = "Genji", Role = HeroRoles.Damage },
        new Hero { Name = "Ana", Role = HeroRoles.Support },
        new Hero { Name = "Sombra", Role = HeroRoles.Damage },
        new Hero { Name = "Orisa", Role = HeroRoles.Tank },
        new Hero { Name = "Doomfist", Role = HeroRoles.Tank },
        new Hero { Name = "Moira", Role = HeroRoles.Support },
        new Hero { Name = "Brigitte", Role = HeroRoles.Support },
        new Hero { Name = "Wrecking Ball", Role = HeroRoles.Tank },
        new Hero { Name = "Ashe", Role = HeroRoles.Damage },
        new Hero { Name = "Baptiste", Role = HeroRoles.Support },
        new Hero { Name = "Sigma", Role = HeroRoles.Tank },
        new Hero { Name = "Echo", Role = HeroRoles.Damage },
        new Hero { Name = "Sojourn", Role = HeroRoles.Damage },
        new Hero { Name = "Junker Queen", Role = HeroRoles.Tank },
        new Hero { Name = "Kiriko", Role = HeroRoles.Support },
        new Hero { Name = "Ramattra", Role = HeroRoles.Tank },
        new Hero { Name = "Lifeweaver", Role = HeroRoles.Support },
        new Hero { Name = "Illari", Role = HeroRoles.Support },
        new Hero { Name = "Mauga", Role = HeroRoles.Tank },
        new Hero { Name = "Venture", Role = HeroRoles.Damage },
        new Hero { Name = "Juno", Role = HeroRoles.Support },
        new Hero { Name = "Hazard", Role = HeroRoles.Tank },
        new Hero { Name = "Freja", Role = HeroRoles.Damage },
        new Hero { Name = "Wuyang", Role = HeroRoles.Support },
        new Hero { Name = "Vendetta", Role = HeroRoles.Damage },
        new Hero { Name = "Domina", Role = HeroRoles.Tank },
        new Hero { Name = "Emre", Role = HeroRoles.Damage },
        new Hero { Name = "Mizuki", Role = HeroRoles.Support },
        new Hero { Name = "Anran", Role = HeroRoles.Damage },
        new Hero { Name = "Jetpack Cat", Role = HeroRoles.Support }
    };

    private const string BASE_API_URL = "https://api.thaseven.com/owstatistics/api"; // EC2 API Instance
    //private string BASE_API_URL = "http://localhost:5000/owstatistics/api"; // Your local API

    [ShowInInspector]
    private async void InitializeAllMaps()
    {
        foreach (Map map in _mapsList)
        {
            try
            {
                await CreateMapAsync(map.Name, map.Mode);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error creating map: {map.Name} with error: {e.Message}");
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
                await CreateHeroAsync(hero.Name, hero.Role);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error creating Hero: {hero.Name} with error: {e.Message}");
            }
        }
    }

    private void Awake()
    {
        SingletonSetup();
    }

    private UnityWebRequest CreatePostWebRequest(string body, string endpoint)
    {
        UnityWebRequest request = new UnityWebRequest(BASE_API_URL + endpoint, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        return request;
    }

    private UnityWebRequest CreateGetWebRequest(string endpoint)
    {
        UnityWebRequest request = new UnityWebRequest(BASE_API_URL + endpoint, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        return request;
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

        string json = JsonConvert.SerializeObject(requestData);

        var request = CreatePostWebRequest(json, "/user/create");

        await request.SendWebRequest();

        GenericResponse response = JsonConvert.DeserializeObject<GenericResponse>(request.downloadHandler.text);
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

        string json = JsonConvert.SerializeObject(requestData);
        var request = CreatePostWebRequest(json, "/map/create");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            MapResponse response = JsonConvert.DeserializeObject<MapResponse>(request.downloadHandler.text);
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

        string json = JsonConvert.SerializeObject(requestData);
        var request = CreatePostWebRequest(json, "/hero/create");
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            HeroResponse response = JsonConvert.DeserializeObject<HeroResponse>(request.downloadHandler.text);
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

        string json = JsonConvert.SerializeObject(requestData);
        var request = CreatePostWebRequest(json, "/user/login");
        await request.SendWebRequest();

        LoginResponse response = JsonConvert.DeserializeObject<LoginResponse>(request.downloadHandler.text);

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
        string json = JsonConvert.SerializeObject(matchData);
        var request = CreatePostWebRequest(json, "/match/create");

        await request.SendWebRequest();

        GenericResponse response = JsonConvert.DeserializeObject<GenericResponse>(request.downloadHandler.text);
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

    public async Task<List<MatchData>> GetMatchListByUserId(int userId)
    {
        UserIdRequest requestData = new UserIdRequest
        {
            UserId = userId
        };

        string json = JsonConvert.SerializeObject(requestData);
        var request = CreatePostWebRequest(json, "/match/get-by-user-id");

        await request.SendWebRequest();

        List<MatchData> response = JsonConvert.DeserializeObject<List<MatchData>>(request.downloadHandler.text);

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Match List retrieved Successfully: {response.Count}");
            return response;
        }
        else
        {
            Debug.LogError($"ERROR: {request.responseCode} with message: {request.error}");
            return null;
        }
    }

    public async Task<(List<FromDatabaseMaps> Maps, List<FromDatabaseHeroes> Heroes)> GetDatabaseData()
    {
        var mapsRequest = CreateGetWebRequest("/map/get-all-maps");

        await mapsRequest.SendWebRequest();
        var maps = JsonConvert.DeserializeObject<List<FromDatabaseMaps>>(mapsRequest.downloadHandler.text);

        if (mapsRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"ERROR RETRIEVING MAPS: {mapsRequest.responseCode} with message: {mapsRequest.error}");
            return (null, null);
        }

        var heroesRequest = CreateGetWebRequest("/hero/get-all-heroes");

        await heroesRequest.SendWebRequest();
        var heroes = JsonConvert.DeserializeObject<List<FromDatabaseHeroes>>(heroesRequest.downloadHandler.text);

        if (heroesRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(
                $"ERROR RETRIEVING HEROES: {heroesRequest.responseCode} with message: {heroesRequest.error}");
            return (null, null);
        }

        Debug.Log($"DatabaseData retrieved Successfully Maps Count: {maps.Count} || Heroes Count = {heroes.Count}");
        return (maps, heroes);
    }
}