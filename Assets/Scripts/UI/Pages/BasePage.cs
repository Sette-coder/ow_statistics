using UnityEngine;

public class BasePage : MonoBehaviour
{
    [SerializeField] private bool _enableOnStart = false;

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
        gameObject.SetActive(true);   
    }
}
