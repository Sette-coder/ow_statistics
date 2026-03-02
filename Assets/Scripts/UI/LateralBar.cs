using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LateralBar : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TextMeshProUGUI _username;
    
    private readonly Vector2 _openPosition = Vector2.zero;
    private Vector2 _closePosition;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _closeButton.onClick.AddListener(Close);
        
        _closePosition = new (_rectTransform.sizeDelta.x, 0);
        _rectTransform.anchoredPosition = _closePosition;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        _username.text = UserDataManager.Instance.GetUsername();
        StartCoroutine(Tha7.Utility.ActionOverTime.LerpObjectPosition2DOverTime(_rectTransform, _closePosition, _openPosition,0.2f));
    }
    
    public void Close()
    {
        StartCoroutine(Tha7.Utility.ActionOverTime.LerpObjectPosition2DOverTime(_rectTransform, _openPosition,_closePosition,0.2f));
    }
}
