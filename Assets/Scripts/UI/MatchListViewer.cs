using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MatchListViewer : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private GameObject _matchElementPrefab;
    [SerializeField] private Button _refreshListButton;

    private List<MatchListElement> _matchListElements = new List<MatchListElement>();

    private void Awake()
    {
        _refreshListButton.onClick.AddListener(RetrieveDataFromDb);

        if (_contentTransform.childCount > 0)
            for (int child = _contentTransform.childCount - 1; child >= 0; child--)
                DestroyImmediate(_contentTransform.GetChild(child).gameObject);

        _matchListElements = new List<MatchListElement>();
        InstantiateMatchListElements();
    }

    void InstantiateMatchListElements(int matchToInstantiate = 30)
    {
        for (int i = 0; i < matchToInstantiate; i++)
        {
            var newMatchElement = Instantiate(_matchElementPrefab, _contentTransform).GetComponent<MatchListElement>();
            newMatchElement.gameObject.SetActive(false);
            _matchListElements.Add(newMatchElement);
        }
    }

    private void OnEnable()
    {
        UpdateList();
    }


    [ShowInInspector]
    public async void RetrieveDataFromDb()
    {
        _refreshListButton.interactable = false;
        await UserDataManager.Instance.UpdateMatchesData();
        _refreshListButton.interactable = true;

        UpdateList();
    }

    void UpdateList()
    {
        var matches = UserDataManager.Instance.GetMatches();

        if (matches.Count == 0) return;
        
        if (matches.Count > _matchListElements.Count)
        {
            InstantiateMatchListElements(matches.Count - _matchListElements.Count);
        }
        
        for (int i = 0; i < _matchListElements.Count; i++)
        {
            if (i < matches.Count)
            {
                var element = matches[i];
                _matchListElements[i].InitializeElements(element);
                _matchListElements[i].gameObject.SetActive(true);
            }
            else
            {
                _matchListElements[i].gameObject.SetActive(false);
            }
        }
    }
}