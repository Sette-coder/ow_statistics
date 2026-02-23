using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

    public class UserDataManager:MonoBehaviour
    {
        public static UserDataManager Instance;

        private string _userEmail;
        private string _username;
        private string _role;
        
        private List<MatchData> _matches = new List<MatchData>();
        
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
                _matches = new List<MatchData>(await ApiClient.Instance.GetMatchListByUsername(_username));
            }
            catch (Exception e)
            {
                Debug.LogError("Error Retrieving Matches: " + e);
            }
        }

        private void SetUserData(object sender, LoginResponse userData)
        {
            _username = userData.Username;
            _userEmail = userData.UserEmail;
            _role = userData.Role;

            _ = UpdateMatchesData();
        }
        
        private void ClearUserData(object sender, EventArgs empty)
        {
            _username = "";
            _userEmail = "";
        }
        
        public string GetUserEmail() => _userEmail;
        public string GetUsername() => _username;

        public List<MatchData> GetMatches() => _matches;
    }
