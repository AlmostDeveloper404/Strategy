using UnityEngine;

public class UnitRecruitmentButton : MonoBehaviour
{
    public GameObject Pref;
    public Transform SpawnPoint;

    Resources resources;

    private void Start()
    {
        resources = Resources.instance;
    }

    public void TryRecruit()
    {
        int wariorCost = Pref.GetComponent<Unit>().Price;
        if (resources.Money>=wariorCost)
        {
            resources.Money -= wariorCost;
            Instantiate(Pref, SpawnPoint.position, SpawnPoint.rotation);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
