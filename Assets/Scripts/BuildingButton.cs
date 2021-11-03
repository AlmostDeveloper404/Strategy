using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public GameObject BuildingPref;
    BuildingPlacer BuildingPlacer;

    Resources Resources;

    private void Start()
    {
        BuildingPlacer = BuildingPlacer.instance;
        Resources = Resources.instance;
    }

    public void TryBuy()
    {
        int price = BuildingPref.GetComponent<Building>().Price;
        if (Resources.Money>=price)
        {
            Resources.Money -= price;
            BuildingPlacer.CreateBuilding(BuildingPref);
        }
    }
}
