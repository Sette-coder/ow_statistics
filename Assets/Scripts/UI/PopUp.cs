using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _messageText;
    
    [SerializeField] private Button _closeButton;
    
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _refuseButton;
    
    [SerializeField] private Color _errorColor = Color.red;
    [SerializeField] private Color _infoColor = Color.white;
    [SerializeField] private Color _successColor = Color.green;
    [SerializeField] private Color _warningColor = Color.yellow;
    
    private Action _closeCallback;
    
    private Action _confirmCallback;
    private Action _refuseCallback;
    
    private void Start()
    {
        _closeButton.onClick.AddListener(Close);
        _confirmButton.onClick.AddListener(Confirm);
        _refuseButton.onClick.AddListener(Refuse);
        
        Close();
    }

    public void Open(PopUpType type, string title, string message, Action callback)
    {
        SetupPopUpPerType(type);
        
        _titleText.text = title;
        _messageText.text = message;
        
        if(callback != null) _closeCallback = callback;
        
        gameObject.SetActive(true);
    }
    
    public void Open(string title, string message, Action confirmCallback, Action refuseCallback)
    {
        SetupPopUpPerType(PopUpType.Info);
        
        _closeButton.gameObject.SetActive(false);
        _confirmButton.gameObject.SetActive(true);
        _refuseButton.gameObject.SetActive(true);
        
        _titleText.text = title;
        _messageText.text = message;
        
        if(confirmCallback != null) _confirmCallback = confirmCallback;
        if(refuseCallback != null) _refuseCallback = refuseCallback;
        
        gameObject.SetActive(true);
    }
    
    public void Open(PopUpType type, string title, string message)
    {
        SetupPopUpPerType(type);
        
        if(type == PopUpType.Error) _messageText.color = _errorColor;
        else if(type == PopUpType.Info) _messageText.color = _infoColor;
        
        _titleText.text = title;
        _messageText.text = message;
        
        gameObject.SetActive(true);
    }

    void Close()
    {
        _closeCallback?.Invoke();
        
        gameObject.SetActive(false);
        _closeCallback = null;
        _confirmCallback = null;
        _refuseCallback = null;
    }

    void Confirm()
    {
        _confirmCallback?.Invoke();
        
        gameObject.SetActive(false);
        _closeCallback = null;
        _confirmCallback = null;
        _refuseCallback = null;
    }
    
    void Refuse()
    {
        _refuseCallback?.Invoke();
        
        gameObject.SetActive(false);
        _closeCallback = null;
        _confirmCallback = null;
        _refuseCallback = null;
    }

    void SetupPopUpPerType(PopUpType type)
    {
        switch (type)
        {
            case PopUpType.Error:
                _titleText.color = _errorColor;
                _messageText.color = _errorColor;
                _closeButton.gameObject.SetActive(true);
                _confirmButton.gameObject.SetActive(false);
                _refuseButton.gameObject.SetActive(false);
                break;
            case PopUpType.Info:
                _titleText.color = _infoColor;
                _messageText.color = _infoColor;
                _closeButton.gameObject.SetActive(true);
                _confirmButton.gameObject.SetActive(false);
                _refuseButton.gameObject.SetActive(false);
                break;
            case PopUpType.Success:
                _titleText.color = _successColor;
                _messageText.color = _successColor;
                _closeButton.gameObject.SetActive(true);
                _confirmButton.gameObject.SetActive(false);
                _refuseButton.gameObject.SetActive(false);
                break;
            case PopUpType.Warning:
                _titleText.color = _warningColor;
                _messageText.color = _warningColor;
                _closeButton.gameObject.SetActive(true);
                _confirmButton.gameObject.SetActive(false);
                _refuseButton.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
