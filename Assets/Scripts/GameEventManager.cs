using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameEventManager:MonoBehaviour
    {
        
        public static GameEventManager Instance;
        private void SingletonSetup()
        {
            if (Instance == null)
                Instance = this;
        }
        private void Awake()
        {
            SingletonSetup();
        }
        
        public EventHandler<LoginResponse> OnLoginSuccess;
        public EventHandler OnLogout;
        
    }
}