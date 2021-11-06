using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    public int Money = 10;
    public Text MoneyText;
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

    public void AddMoney()
    {
        Money++;
        MoneyText.text = "Δενόγθ: " + Money;
    }


}
