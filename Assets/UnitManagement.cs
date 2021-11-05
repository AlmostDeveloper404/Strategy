using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManagement : MonoBehaviour
{
    public static UnitManagement instance;

    #region Singleton
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
            Debug.LogWarning("More than one UnitManagement!");
            return;
        }
        instance = this;
    }
    #endregion


    public List<Unit> currentUnitsInScene = new List<Unit>();


    public void AddUnitToList(Unit unit)
    {
        currentUnitsInScene.Add(unit);
    }

    public void RemoveUnitFromList(Unit unit)
    {
        currentUnitsInScene.Remove(unit);
    }
}
