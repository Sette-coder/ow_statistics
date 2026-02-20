using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : BasePage
{
    [SerializeField] TMP_InputField _usernameInputField;
    [SerializeField] TMP_InputField _passwordInputField;
    [SerializeField] Button _loginButton;
    
    [SerializeField] Button _createUserButton;
    
    [SerializeField] BasePage _createUserPage;
    [SerializeField] BasePage _homePage;
    

    protected override void Start()
    {
        _loginButton.onClick.AddListener(LoginDataCheck);
        _createUserButton.onClick.AddListener(() =>
        {
            _createUserPage.EnablePage();
            DisablePage();
        });
        
        _usernameInputField.onValueChanged.AddListener(InputFieldCheck);
        _passwordInputField.onValueChanged.AddListener(InputFieldCheck);

        InputFieldCheck("");
        
        base.Start();
    }
    
    private async void LoginDataCheck()
    {
        if (string.IsNullOrEmpty(_usernameInputField.text))
        {
            UiManager.Instance.OpenPopUp(
                PopUpType.Error,
                "Error",
                "Username field is empty, please enter a username or email"
                );
            return;
        }
        
        if (string.IsNullOrEmpty(_passwordInputField.text))
        {
            UiManager.Instance.OpenPopUp(
                PopUpType.Error,
                "Error",
                "Password field is empty, please enter your password"
            );
            return;
        }

        var loginCheck = await ApiClient.Instance.TryLogin(_usernameInputField.text, _passwordInputField.text);

        if (!loginCheck.Authorized)
        {
            UiManager.Instance.OpenPopUp(
                PopUpType.Error,
                "Error",
                loginCheck.LoginMessage
            );
        }
        else
        {
            _homePage.EnablePage();
            DisablePage();
        }
    }
    
    public override void DisablePage()
    {
        _usernameInputField.text = ""; 
        _passwordInputField.text = "";
        
        _loginButton.interactable = false;
        
        base.DisablePage();
    }
    
    public override void EnablePage()
    {
        _usernameInputField.text = ""; 
        _passwordInputField.text = "";
        _loginButton.interactable = false;
        
        base.EnablePage();
    }
    
    private void InputFieldCheck(string value)
    {
        if (string.IsNullOrWhiteSpace(_usernameInputField.text))
        {
            _loginButton.interactable = false;
            return;
        }
        
        if (string.IsNullOrWhiteSpace(_passwordInputField.text))
        {
            _loginButton.interactable = false;
            return;
        }
        
        _loginButton.interactable = true;
    }
}
