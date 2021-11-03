using UnityEngine;

public  class Resources : MonoBehaviour
{
    public int Money=10;
    #region Singleton
    public static Resources instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion 

    
}
