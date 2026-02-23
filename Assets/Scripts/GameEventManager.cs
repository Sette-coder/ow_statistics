using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
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