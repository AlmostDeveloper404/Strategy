using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Barack : Building
{
    public GameObject BarackCanvas;

    public Transform SpawnPoint;

    Resources resources;

    public Transform Cells;
    Cell[] slots;

    public List<Sprite> wariorsInQueue = new List<Sprite>();




    public override void Start()
    {
        base.Start();
        resources = Resources.instance;
        slots = Cells.GetComponentsInChildren<Cell>();
    }
    public override void Select()
    {
        base.Select();
        BarackCanvas.SetActive(true);
    }

    public override void Diselect()
    {
        base.Diselect();
        BarackCanvas.SetActive(false);
    }
    public void TryRecruit(GameObject pref, Sprite unitImage)
    {
        Unit foundedUnit = pref.GetComponent<Unit>();
        int wariorCost = foundedUnit.Price;
        float timeForCreation = foundedUnit.TimeForCreation;
        if (resources.Money >= wariorCost)
        {
            if (wariorsInQueue.Count < slots.Length)
            {
                wariorsInQueue.Add(unitImage);
                for (int i = 0; i < slots.Length; i++)
                {
                    if (i<wariorsInQueue.Count)
                    {
                        slots[i].CreateWarior(unitImage,pref,SpawnPoint,wariorCost,timeForCreation);
                        
                    }
                }
            }
            else
            {
                Debug.Log("NotEnoughRoom");
                return;
            }
            
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    
}
