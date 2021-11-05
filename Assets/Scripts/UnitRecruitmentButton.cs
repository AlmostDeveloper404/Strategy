using UnityEngine;

public class UnitRecruitmentButton : MonoBehaviour
{
    public GameObject Pref;
    public Transform SpawnPoint;

    Resources resources;
    UnitManagement unitManagement;

    private void Start()
    {
        unitManagement = UnitManagement.instance;
        resources = Resources.instance;
    }

    public void TryRecruit()
    {
        int wariorCost = Pref.GetComponent<Unit>().Price;
        if (resources.Money>=wariorCost)
        {
            resources.Money -= wariorCost;
            GameObject newUnit= Instantiate(Pref, SpawnPoint.position, SpawnPoint.rotation);
            unitManagement.AddUnitToList(newUnit.GetComponent<Unit>());
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
