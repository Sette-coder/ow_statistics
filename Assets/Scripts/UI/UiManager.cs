using System;
using JetBrains.Annotations;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    private void SingletonSetup()
    {
        if (Instance == null)
            Instance = this;
    }
    
    [SerializeField] private PopUp _popUp;

    private void Awake()
    {
        SingletonSetup();
    }

    public void OpenPopUp(PopUpType type, string title, string message, Action callback)
    {
        _popUp.Open(type, title, message, callback);
    }
    
    public void OpenPopUp(PopUpType type, string title, string message)
    {
        _popUp.Open(type, title, message);
    }
    
    public void OpenPopUp(string title, string message, Action confirmCallback, Action refuseCallback)
    {
        _popUp.Open(title, message, confirmCallback, refuseCallback);
    }
}
