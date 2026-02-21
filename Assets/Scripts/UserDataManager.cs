using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class UserDataManager:MonoBehaviour
    {
        public static UserDataManager Instance;

        private string _userEmail;
        private string _username;
        private string _role;
        
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

        private void SetUserData(object sender, LoginResponse userData)
        {
            _username = userData.Username;
            _userEmail = userData.UserEmail;
            _role = userData.Role;
        }
        
        private void ClearUserData(object sender, EventArgs empty)
        {
            _username = "";
            _userEmail = "";
        }
        
        public string GetUserEmail() => _userEmail;
        public string GetUsername() => _username;
    }
}