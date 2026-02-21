using UnityEngine;
using UnityEngine.Serialization;

public class BasePage : MonoBehaviour
{
    [SerializeField] private bool _enableOnStart = false;
    [FormerlySerializedAs("_topBar")] [SerializeField] private BottomBar bottomBar;
    [SerializeField] private string _pageName;

    protected virtual void Start()
    {
        gameObject.SetActive(_enableOnStart);
    }
    public virtual void DisablePage()
    {
        gameObject.SetActive(false);   
    }
    public virtual void EnablePage()
    {
        if(bottomBar) bottomBar.UpdatePageTitle(_pageName);
        gameObject.SetActive(true);
    }
}
