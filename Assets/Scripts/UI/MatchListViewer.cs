using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MatchListViewer : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private GameObject _matchElementPrefab;
    [SerializeField] private Button _refreshListButton;

    private void Awake()
    {
        _refreshListButton.onClick.AddListener(RetrieveDataFromDb);
    }

    [ShowInInspector]
    public async void RetrieveDataFromDb()
    {
        _refreshListButton.interactable = false;
        var matches = await ApiClient.Instance.GetMatchListByEmail(UserDataManager.Instance.GetUserEmail());
        if (matches != null)
        {
            InitializeList(matches);
        }
        _refreshListButton.interactable = true;
    }

    void InitializeList(List<MatchResponse> matches)
    {
        if (_contentTransform.childCount > 0)
            for (int child = _contentTransform.childCount - 1; child >= 0; child--)
                DestroyImmediate(_contentTransform.GetChild(child).gameObject);

        foreach (var match in matches)
        {
            var newMatchElement = Instantiate(_matchElementPrefab, _contentTransform);
            newMatchElement.GetComponent<MatchListElement>().InitializeElements(match);
        }
    }
}