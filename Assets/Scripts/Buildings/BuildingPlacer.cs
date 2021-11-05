using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public float CellSize = 1f;

    public Camera RaycastCamera;
    Plane plane;

    public Building CurrentBuilding;

    public Dictionary<Vector2Int, Building> BuildingsPos = new Dictionary<Vector2Int, Building>();

    public List<Building> amountOfBuildings = new List<Building>();

    
    #region Singleton
    public static BuildingPlacer instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion 
    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
        for (int i = 0; i < amountOfBuildings.Count; i++)
        {
            int xCoordinate = Mathf.RoundToInt(amountOfBuildings[i].transform.position.x);
            int zCoordinate = Mathf.RoundToInt(amountOfBuildings[i].transform.position.z);
            InstallBuilding(xCoordinate,zCoordinate,amountOfBuildings[i]);
        }
    }
    private void Update()
    {
        if (CurrentBuilding==null)
        {
            return;
        }
        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        plane.Raycast(ray, out distance);

        Vector3 point = ray.GetPoint(distance)/CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        CurrentBuilding.transform.position = new Vector3(x,0f,z)*CellSize;

        if (CanBuild(x,z,CurrentBuilding))
        {
            CurrentBuilding.DisplayAccecibleBuild();
            if (Input.GetMouseButtonDown(0))
            {
                InstallBuilding(x, z, CurrentBuilding);
                CurrentBuilding = null;
            }
        }
        else
        {
            CurrentBuilding.DisplayUnaccecibleBuild();
        }
    }


    bool CanBuild(int xCoordinate, int zCoordinate, Building BuildingKey)
    {
        for (int x = 0; x < BuildingKey.XCellSize; x++)
        {
            for (int z = 0; z < BuildingKey.ZCellSize; z++)
            {
                Vector2Int Coordinate = new Vector2Int(xCoordinate + x, zCoordinate + z);
                if (BuildingsPos.ContainsKey(Coordinate))
                {
                    return false;
                }
            }
        }

        return true;
    }

    void InstallBuilding(int xCoordinate,int zCoordinate,Building BuildingKey)
    {
        for (int x = 0; x < BuildingKey.XCellSize; x++)
        {
            for (int z = 0; z < BuildingKey.ZCellSize; z++)
            {
                Vector2Int Coordinate = new Vector2Int(xCoordinate+x,zCoordinate+z);
                BuildingsPos.Add(Coordinate,BuildingKey);
            }
        }
    }
   

    public void CreateBuilding(GameObject buildingPref)
    {
        GameObject newBuilding= Instantiate(buildingPref);
        CurrentBuilding = newBuilding.GetComponent<Building>();
        AddToList(CurrentBuilding);
    }

    public void AddToList(Building createdBuilding)
    {
        amountOfBuildings.Add(createdBuilding);
    }

    public void RemoveFromList(Building destroyedBuilding)
    {
        amountOfBuildings.Add(destroyedBuilding);
    }
}
