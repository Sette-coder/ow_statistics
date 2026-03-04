using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MatchListViewer : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private GameObject _matchElementPrefab;
    [SerializeField] private Button _refreshListButton;

    private List<MatchListElement> _matchListElements = new();

    private void Awake()
    {
        _refreshListButton.onClick.AddListener(RetrieveDataFromDb);
        ClearContent();
        InstantiateMatchListElements();
    }

    private void OnEnable()
    {
        // Wait for data to be ready before populating — avoids empty list flash on first open
        if (UserDataManager.Instance.IsDataReady)
            UpdateList();
    }

    private void Start()
    {
        // Subscribe after Awake so we don't miss the event if data loads fast
        GameEventManager.Instance.OnLoginSuccess += OnLoginSuccess;
    }

    private void OnDestroy()
    {
        GameEventManager.Instance.OnLoginSuccess -= OnLoginSuccess;
    }

    // Triggered when login completes and data finishes loading
    private void OnLoginSuccess(object sender, LoginResponse response)
    {
        UpdateList();
    }

    // ─── Refresh button ────────────────────────────────────
    [ShowInInspector]
    public async void RetrieveDataFromDb()
    {
        _refreshListButton.interactable = false;

        await UserDataManager.Instance.UpdateMatchesDataAsync(); // updated method name
        UpdateList();

        _refreshListButton.interactable = true;
    }

    // ─── List management ───────────────────────────────────
    private void UpdateList()
    {
        var matches = UserDataManager.Instance.GetMatches();

        if (matches == null || matches.Count == 0)
        {
            HideAllElements();
            return;
        }

        // Instantiate more elements only if needed
        if (matches.Count > _matchListElements.Count)
            InstantiateMatchListElements(matches.Count - _matchListElements.Count);

        for (var i = 0; i < _matchListElements.Count; i++)
            if (i < matches.Count)
            {
                _matchListElements[i].InitializeElements(matches[i]);
                _matchListElements[i].gameObject.SetActive(true);
            }
            else
            {
                _matchListElements[i].gameObject.SetActive(false);
            }
    }

    private void InstantiateMatchListElements(int count = 30)
    {
        for (var i = 0; i < count; i++)
        {
            var element = Instantiate(_matchElementPrefab, _contentTransform)
                .GetComponent<MatchListElement>();
            element.gameObject.SetActive(false);
            _matchListElements.Add(element);
        }
    }

    private void HideAllElements()
    {
        foreach (var element in _matchListElements)
            element.gameObject.SetActive(false);
    }

    private void ClearContent()
    {
        for (var i = _contentTransform.childCount - 1; i >= 0; i--)
            DestroyImmediate(_contentTransform.GetChild(i).gameObject);

        _matchListElements.Clear();
    }
}