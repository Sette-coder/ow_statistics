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
    
    [SerializeField] private Color _errorColor = Color.red;
    [SerializeField] private Color _infoColor = Color.white;
    [SerializeField] private Color _successColor = Color.green;
    [SerializeField] private Color _warningColor = Color.yellow;
    
    private Action closeCallback;
    
    private void Start()
    {
        _closeButton.onClick.AddListener(Close);
        Close();
    }

    public void Open(PopUpType type, string title, string message, Action callback)
    {
        switch (type)
        {
            case PopUpType.Error:
                _titleText.color = _errorColor;
                _messageText.color = _errorColor;
                break;
            case PopUpType.Info:
                _titleText.color = _infoColor;
                _messageText.color = _infoColor;
                break;
            case PopUpType.Success:
                _titleText.color = _successColor;
                _messageText.color = _successColor;
                break;
            case PopUpType.Warning:
                _titleText.color = _warningColor;
                _messageText.color = _warningColor;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        if (type == PopUpType.Error)
        {
            _titleText.color = _errorColor;
            _messageText.color = _errorColor;
        }
        else if (type == PopUpType.Info)
        {
            _titleText.color = _infoColor;
            _messageText.color = _infoColor;
        }
        
        _titleText.text = title;
        _messageText.text = message;
        
        if(callback != null) closeCallback = callback;
        
        gameObject.SetActive(true);
    }
    
    public void Open(PopUpType type, string title, string message)
    {
        if(type == PopUpType.Error) _messageText.color = _errorColor;
        else if(type == PopUpType.Info) _messageText.color = _infoColor;
        
        _titleText.text = title;
        _messageText.text = message;
        
        gameObject.SetActive(true);
    }

    void Close()
    {
        closeCallback?.Invoke();
        
        gameObject.SetActive(false);
        closeCallback = null;
    }
}
