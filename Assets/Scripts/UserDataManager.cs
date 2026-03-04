using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance;

    // ─── Tokens ────────────────────────────────────────────────
    private DateTime _accessTokenExpiry;
    [ShowInInspector] private List<FromDatabaseHeroes> _heroes = new();
    [ShowInInspector] private List<FromDatabaseMaps> _maps = new();

    // ─── Cached data ───────────────────────────────────────
    [ShowInInspector] private List<MatchDto> _matches = new();
    private string _role;
    private string _userEmail;

    // ─── User data ─────────────────────────────────────────
    private int _userId;
    private string _username;

    // ─── State ─────────────────────────────────────────────
    public bool IsLoggedIn { get; private set; } // private set — only this class should change it
    public bool IsDataReady { get; private set; } // true once maps + heroes are loaded
    public string AccessToken { get; private set; }

    public string RefreshToken { get; private set; }

    public bool TokenIsValid => IsLoggedIn && DateTime.UtcNow < _accessTokenExpiry.AddMinutes(-5);


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // survives scene loads
    }

    private void Start()
    {
        GameEventManager.Instance.OnLoginSuccess += OnLoginSuccess;
        GameEventManager.Instance.OnLogout += OnLogout;
    }

    private void OnDestroy()
    {
        // Always unsubscribe to avoid ghost callbacks after scene unload
        GameEventManager.Instance.OnLoginSuccess -= OnLoginSuccess;
        GameEventManager.Instance.OnLogout -= OnLogout;
    }

    public void StoreTokens(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        _accessTokenExpiry = DateTime.UtcNow.AddMinutes(120); // match your API setting
    }

    private void ClearTokens()
    {
        AccessToken = "";
        RefreshToken = "";
        _accessTokenExpiry = DateTime.MinValue;
    }

    // ─── Event handlers ────────────────────────────────────
    private void OnLoginSuccess(object sender, LoginResponse userData)
    {
        _userId = userData.UserId;
        _username = userData.Username;
        _userEmail = userData.UserEmail;
        _role = userData.Role;

        // Tokens live in AuthManager — no need to duplicate them here
        IsLoggedIn = true;
        IsDataReady = false;

        StoreTokens(userData.AccessToken, userData.RefreshToken);

        _ = LoadAllDataAsync();
    }

    private void OnLogout(object sender, EventArgs e)
    {
        _userId = -1;
        _username = "";
        _userEmail = "";
        _role = "";

        _matches.Clear();
        _maps.Clear();
        _heroes.Clear();

        IsLoggedIn = false;
        IsDataReady = false;
    }

    // ─── Data loading ──────────────────────────────────────

    // Runs both requests in parallel instead of sequentially
    private async Task LoadAllDataAsync()
    {
        try
        {
            var matchesTask = UpdateMatchesDataAsync();
            var baseDataTask = LoadBaseDataAsync();

            await Task.WhenAll(matchesTask, baseDataTask);

            IsDataReady = true;
            Debug.Log("All data loaded successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading data after login: {e}");
        }
    }

    public async Task UpdateMatchesDataAsync()
    {
        try
        {
            var result = await ApiClient.Instance.GetMatchListByUserId(_userId);
            if (result == null)
            {
                Debug.LogWarning("Match list returned null — keeping previous data.");
                return;
            }

            _matches = new List<MatchDto>(result);
            Debug.Log($"Matches updated — count: {_matches.Count}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving matches: {e}");
        }
    }

    [ShowInInspector]
    private async Task LoadBaseDataAsync()
    {
        try
        {
            var response = await ApiClient.Instance.GetDatabaseData();
            if (response.Maps == null || response.Heroes == null)
            {
                Debug.LogWarning("Base data returned null — maps/heroes not loaded.");
                return;
            }

            _maps = new List<FromDatabaseMaps>(response.Maps);

            // Hero list always starts with an empty entry as a "none selected" placeholder
            _heroes = new List<FromDatabaseHeroes>
            {
                new() { Id = -1, Name = "", Role = "" }
            };
            _heroes.AddRange(response.Heroes);

            Debug.Log($"Base data loaded — maps: {_maps.Count} | heroes: {_heroes.Count}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving base data: {e}");
        }
    }

    // ─── Getters ───────────────────────────────────────────
    public int GetUserId() => _userId;
    public string GetUsername() => _username;
    public string GetUserEmail() => _userEmail;
    public string GetRole() => _role;
    public List<MatchDto> GetMatches() => _matches;
    public List<FromDatabaseMaps> GetMapList() => _maps;
    public List<FromDatabaseHeroes> GetHeroesList() => _heroes;
}