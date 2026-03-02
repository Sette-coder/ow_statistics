using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

    public class UserDataManager:MonoBehaviour
    {
        public static UserDataManager Instance;

        private int _userId;
        private string _userEmail;
        private string _username;
        private string _role;
        
        private List<MatchData> _matches = new List<MatchData>();
        [ShowInInspector]
        private List<FromDatabaseMaps> _maps = new List<FromDatabaseMaps>();
        [ShowInInspector]
        private List<FromDatabaseHeroes> _heroes = new List<FromDatabaseHeroes>();
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            GameEventManager.Instance.OnLoginSuccess += SetUserData;
            GameEventManager.Instance.OnLogout += ClearUserData;
        }

        public async Task UpdateMatchesData()
        {
            try
            {
                _matches = new List<MatchData>(await ApiClient.Instance.GetMatchListByUserId(_userId));
            }
            catch (Exception e)
            {
                Debug.LogError("Error Retrieving Matches: " + e);
            }
        }
        [ShowInInspector]
        private async Task GetBaseDataFromDatabase()
        {
            try
            {
                var response = await ApiClient.Instance.GetDatabaseData();
                _heroes = new List<FromDatabaseHeroes>();
                
                _heroes.Add(new FromDatabaseHeroes()
                {
                    Id = -1,
                    Name = "",
                    Role = ""
                });
                
                _heroes.AddRange(response.Heroes);
                _maps = new List<FromDatabaseMaps>(response.Maps);
            }
            catch (Exception e)
            {
                Debug.LogError("Error Retrieving Database DAta: " + e);
            }
        }

        private void SetUserData(object sender, LoginResponse userData)
        {
            _userId = userData.UserId;
            _username = userData.Username;
            _userEmail = userData.UserEmail;
            _role = userData.Role;

            _ = UpdateMatchesData();
            _ = GetBaseDataFromDatabase();
        }
        
        private void ClearUserData(object sender, EventArgs empty)
        {
            _username = "";
            _userEmail = "";
        }
        
        public string GetUserEmail() => _userEmail;
        public string GetUsername() => _username;
        public int GetUserId() => _userId;
        public List<FromDatabaseMaps> GetMapList() => _maps;
        public List<FromDatabaseHeroes> GetHeroesList() => _heroes;

        public List<MatchData> GetMatches() => _matches;
    }
