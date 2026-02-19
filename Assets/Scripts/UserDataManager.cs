using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class UserDataManager:MonoBehaviour
    {
        public static UserDataManager Instance;

        private string _userEmail;
        private string _username;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            GameEventManager.Instance.OnLoginSuccess += SetUserData;
        }

        private void SetUserData(object sender, (string username, string userEmail)userData)
        {
            _username = userData.username;
            _userEmail = userData.userEmail;
        }
        
        public string GetUserEmail() => _userEmail;
        public string GetUsername() => _username;
    }
}