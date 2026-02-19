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

        public EventHandler<(string username, string userEmail)> OnLoginSuccess;
        
        private void Awake()
        {
            SingletonSetup();
        }
        
        
    }
}