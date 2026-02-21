using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateUserPage : BasePage
{
    [SerializeField] TMP_InputField _usernameInputField;
    [SerializeField] TMP_InputField _emailInputField;
    [SerializeField] TMP_InputField _passwordInputField;
    [SerializeField] Button _createUserButton;

    [SerializeField] Button _backToLoginButton;

    [SerializeField] LoginPage _loginPage;

    protected override void Start()
    {
        _createUserButton.onClick.AddListener(CreateNewUserCheck);
        _backToLoginButton.onClick.AddListener(() =>
        {
            UiManager.Instance.ChangePage(_loginPage);
        });

        _usernameInputField.onValueChanged.AddListener(InputFieldCheck);
        _emailInputField.onValueChanged.AddListener(InputFieldCheck);
        _passwordInputField.onValueChanged.AddListener(InputFieldCheck);

        InputFieldCheck("");
        
        base.Start();
    }

    private async void CreateNewUserCheck()
    {
        if (string.IsNullOrEmpty(_usernameInputField.text))
        {
            UiManager.Instance.OpenPopUp(
                PopUpType.Error,
                "Error",
                "Username field is empty, please enter a username"
            );
            return;
        }

        if (string.IsNullOrEmpty(_emailInputField.text))
        {
            UiManager.Instance.OpenPopUp(
                PopUpType.Error,
                "Error",
                "Email field is empty, please enter a valid email address"
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

        var createUserCheck = await ApiClient.Instance.CreateUserAsync(
            _usernameInputField.text,
            _emailInputField.text,
            _passwordInputField.text);

        if (!createUserCheck.ok)
        {
            UiManager.Instance.OpenPopUp(
                PopUpType.Error,
                "Error",
                createUserCheck.ResponseMessage
            );
        }
        else
        {
            UiManager.Instance.OpenPopUp(
                PopUpType.Success,
                "User Created",
                "User Created Successfully",
                () =>
                    {
                        UiManager.Instance.ChangePage(_loginPage);
                    });
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

        if (!IsValidEmail(_emailInputField.text))
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

    bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase
        );
    }
}