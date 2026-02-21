using System;
using UnityEngine;

public class ScrollViewFollower : MonoBehaviour
{
    [SerializeField] RectTransform _content;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = new Vector2(_content.anchoredPosition.x, 0);
    }
}
