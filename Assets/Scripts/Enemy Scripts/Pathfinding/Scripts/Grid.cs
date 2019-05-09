using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public LayerMask wallMask;
    public Vector2 worldSize;
    public float tileRad;
    public float spawnDist;

    GridNodes[,] grid;
    public List<GridNodes> foundPath;

    float tileDiameter;
    int gridSizeX;
    int gridSizeY;


    private void Start()
    {
        tileDiameter = tileRad * 2;
        gridSizeX = Mathf.RoundToInt(worldSize.x / tileDiameter);
        gridSizeY = Mathf.RoundToInt(worldSize.y / tileDiameter);
        gridGen();
        if (grid != null)
        {
            Debug.Log("Grid Initialised");
        }
    }

    void gridGen()
    {
        grid = new GridNodes[gridSizeX, gridSizeY];

        Vector3 botLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2; //find the bottom left

       // Debug.Log(botLeft);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 nodeWordlPos = botLeft + Vector3.right * (x * tileDiameter + tileRad) + Vector3.forward * (y * tileDiameter + tileRad); // finding the node tiles world pos

                //Debug.Log(nodeWordlPos);

                bool Wall = true;

                if (Physics.CheckSphere(nodeWordlPos, tileRad, wallMask))
                {
                    Wall = false;
                }

                grid[x,y] = new GridNodes(Wall, nodeWordlPos, x,y);

              // Debug.Log(nodeWordlPos.x);
              //Debug.Log(nodeWordlPos.y);
            }
        }
    }

    public GridNodes NodeFromWorldPos(Vector3 cWorldPos)
    {
        float xPoint = ((cWorldPos.x + worldSize.x / 2) / worldSize.x);
        float yPoint = ((cWorldPos.z + worldSize.y / 2) / worldSize.y);

        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];
    }

    public List<GridNodes> GetNeighbours(GridNodes node)
    {
        List<GridNodes> neighbours = new List<GridNodes>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
               
                if (x == 0 && y == 0)
                {
                    continue;
                }
               // Debug.Log(node.gridPosX);
                int checkX = node.gridPosX + x;
                int checkY = node.gridPosY + y;
               
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                    
                }
            }
        }
        return neighbours;
    }

    //private void OnDrawGizmos() //showing the grid 
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));
    //    //Debug.Log("it happened");
    //    if (grid != null)
    //    {
    //        foreach (GridNodes nodes in grid)
    //        {
    //            if (nodes.isObstructed)
    //            {
    //                Gizmos.color = Color.white;
    //            }
    //            else
    //            {
    //                Gizmos.color = Color.blue;
    //            }

    //            if (foundPath != null)
    //            {
    //                if (foundPath.Contains(nodes))
    //                    Gizmos.color = Color.red;
    //            }

    //            Gizmos.DrawCube(nodes.pos, Vector3.one * (tileDiameter - spawnDist));
    //            // Debug.Log("all in");
    //        }
    //    }
    //}


}
