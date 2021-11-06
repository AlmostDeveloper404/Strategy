using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Image WariorIcon; 
    
    UnitManagement unitManagement;
    Resources resources;

    public Barack Barack;

    float _timer;

    GameObject _pref;
    Transform spawnPoint;
    int _wariorCost;
    float _timeForCreation;
    


    private void Start()
    {
        _timer = 0.1f;
        unitManagement = UnitManagement.instance;
        resources = Resources.instance;
    }


    private void Update()
    {
        if (!_pref)
        {
            return;
        }
        _timer -= Time.deltaTime;

        
        if (_timer<0)
        {
            Create(_pref,spawnPoint,_wariorCost,_timeForCreation);
        }
    }


    public void CreateWarior(Sprite wariorIcon,GameObject pref,Transform SpawnPoint,int wariorCost,float timeForCreation)
    {
        WariorIcon.sprite = wariorIcon;
        _timer = timeForCreation;
        _pref = pref;

        spawnPoint = SpawnPoint;
        _wariorCost = wariorCost;
        _timeForCreation = timeForCreation;


    }

    void Create(GameObject pref, Transform SpawnPoint, int wariorCost, float timeForCreation)
    {
        resources.Money -= wariorCost;
        float x = Random.Range(0f, 2f);
        float z = Random.Range(0f, 2f);

        Vector3 offset = new Vector3(x, 0f, z);
        GameObject newUnit = Instantiate(pref, SpawnPoint.position + offset, SpawnPoint.rotation);
        unitManagement.AddUnitToList(newUnit.GetComponent<Unit>());
        _pref = null;
        WariorIcon.sprite = null;
        Barack.wariorsInQueue.Remove(WariorIcon.sprite);
    }

}
