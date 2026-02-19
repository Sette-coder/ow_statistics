using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateUserPage : BasePage
{
    [SerializeField] TextMeshProUGUI _errorText;

    [SerializeField] TMP_InputField _usernameInputField;
    [SerializeField] TMP_InputField _emailInputField;
    [SerializeField] TMP_InputField _passwordInputField;
    [SerializeField] Button _createUserButton;

    [SerializeField] Button _backToLoginButton;

    [SerializeField] LoginPage _loginPage;

    private void Start()
    {
        _errorText.text = string.Empty;

        _createUserButton.onClick.AddListener(CreateNewUserCheck);
        _backToLoginButton.onClick.AddListener(() =>
        {
            _loginPage.EnablePage();
            DisablePage();
        });

        _usernameInputField.onValueChanged.AddListener(InputFieldCheck);
        _emailInputField.onValueChanged.AddListener(InputFieldCheck);
        _passwordInputField.onValueChanged.AddListener(InputFieldCheck);
        
        InputFieldCheck("");
    }

    private async void CreateNewUserCheck()
    {
        if (string.IsNullOrEmpty(_usernameInputField.text))
        {
            _errorText.text = "Username field is empty, please enter a username";
            return;
        }

        if (string.IsNullOrEmpty(_emailInputField.text))
        {
            _errorText.text = "Email field is empty, please enter a valid email address";
            return;
        }

        if (string.IsNullOrEmpty(_passwordInputField.text))
        {
            _errorText.text = "Password field is empty, please enter your password";
            return;
        }

        var createUserCheck = await ApiClient.Instance.CreateUserAsync(
            _usernameInputField.text, 
            _emailInputField.text,
            _passwordInputField.text);

        if (!createUserCheck.ok)
        {
            _errorText.text = createUserCheck.ResponseMessage;
        }
        else
        {
            _loginPage.EnablePage();
            DisablePage();
        }
    }

    public override void DisablePage()
    {
        _usernameInputField.text = ""; 
        _emailInputField.text = ""; 
        _passwordInputField.text = "";
        
        _createUserButton.interactable = false;
        
        base.DisablePage();
    }
    
    public override void EnablePage()
    {
        _usernameInputField.text = ""; 
        _emailInputField.text = ""; 
        _passwordInputField.text = "";
        
        _createUserButton.interactable = false;
        
        base.EnablePage();
    }

    private void InputFieldCheck(string value)
    {
        if (string.IsNullOrWhiteSpace(_usernameInputField.text))
        {
            _createUserButton.interactable = false;
            return;
        }
        
        if (string.IsNullOrWhiteSpace(_emailInputField.text))
        {
            _createUserButton.interactable = false;
            return;
        }
        
        if (string.IsNullOrWhiteSpace(_passwordInputField.text))
        {
            _createUserButton.interactable = false;
            return;
        }
        
        _createUserButton.interactable = true;
    }
}