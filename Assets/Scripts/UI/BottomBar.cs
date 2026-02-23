using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _usernameText;
    [SerializeField] TextMeshProUGUI _pageTitleText;
    [SerializeField] GameObject _content;
    
    [SerializeField] Button _logoutButton;
    [SerializeField] Button _openLateralMenuButton;
    
    [SerializeField] LateralBar _lateralBar;
    
    [SerializeField] BasePage _loginPage;

    private void Start()
    {
        GameEventManager.Instance.OnLoginSuccess += OnLoginSuccess;
        _logoutButton.onClick.AddListener(Logout);
        _openLateralMenuButton.onClick.AddListener(_lateralBar.Open);
        _content.SetActive(false);
    }
    
    private void OnLoginSuccess(object sender ,LoginResponse loginResponse)
    {
        _usernameText.text = loginResponse.Username;
        _content.SetActive(true);
    }

    void Logout()
    {
        UiManager.Instance.OpenPopUp(
            "Logout",
            "Do you really want to logout?",
            () =>
            {
                UiManager.Instance.ChangePage(_loginPage);
                _content.SetActive(false);
                GameEventManager.Instance.OnLogout?.Invoke(this,EventArgs.Empty);
            },
            ()=>{}
            );
        
    }
    
    public void UpdatePageTitle(string pageTitle)
    {
        _pageTitleText.text = pageTitle;
    }
}
