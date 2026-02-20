using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI _usernameText;
    [SerializeField] Button _logoutButton;
    
    [SerializeField] BasePage _homePage;
    [SerializeField] BasePage _loginPage;

    private void Start()
    {
        GameEventManager.Instance.OnLoginSuccess += OnLoginSuccess;
        _logoutButton.onClick.AddListener(Logout);
    }
    
    private void OnLoginSuccess(object sender ,(string username, string userEmail)userData)
    {
        _usernameText.text = userData.username;
    }

    void Logout()
    {
        UiManager.Instance.OpenPopUp(
            "Logout",
            "Do you really want to logout?",
            () =>
            {
                _homePage.DisablePage();
                _loginPage.EnablePage();
                
                GameEventManager.Instance.OnLogout?.Invoke(this,EventArgs.Empty);
            },
            ()=>{}
            );
        
    }
}
