using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public GameObject Indicator;

    public virtual void Start()
    {
        Indicator.SetActive(false);
    }

    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public virtual void OnUnhover()
    {
        transform.localScale = Vector3.one;
    }
    public virtual void Select()
    {
        Indicator.SetActive(true);
    }

    public virtual void Diselect()
    {
        Indicator.SetActive(false);
    }

    public virtual void ClickOnGround(Vector3 point)
    {

    }

    
}
