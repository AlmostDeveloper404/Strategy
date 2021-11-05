using UnityEngine;
using System.Collections.Generic;


public class Building : SelectableObject
{
    public int Price;
    public int XCellSize;
    public int ZCellSize;

    Color startColor;
    public Renderer Renderer;

   
    private void Awake()
    {
        startColor = Renderer.material.color;
    }

    private void OnDrawGizmos()
    {
        float CellSize = FindObjectOfType<BuildingPlacer>().CellSize;
        for (int x = 0; x < XCellSize; x++)
        {
            for (int z = 0; z < ZCellSize; z++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(x, 0f, z) * CellSize, new Vector3(1f, 0f, 1f) * CellSize);
            }
        }

    }
    public void DisplayUnaccecibleBuild()
    {
        Renderer.material.color = Color.red;
    }
    public void DisplayAccecibleBuild()
    {
        Renderer.material.color=startColor;
    }
}
