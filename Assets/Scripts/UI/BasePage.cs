using UnityEngine;

public class BasePage : MonoBehaviour
{
    public virtual void DisablePage()
    {
        gameObject.SetActive(false);   
    }
    public virtual void EnablePage()
    {
        gameObject.SetActive(true);   
    }
}
