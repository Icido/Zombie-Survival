using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

    Grid grid;

    public List<Vector3> storedPath = new List<Vector3>();

    public bool finishedPath = false;
    //public GameObject startPos;
   // public GameObject targetPos;

    private void Awake()
    {
        grid = GameObject.Find("Terrain").GetComponent<Grid>();

        //grid = GetComponent<Grid>();
    }

    //private void Update()
    //{
    //    pathFind(startPos.position, targetPos.position);
    //}

    public void pathFind(Vector3 cStartPos, Vector3 cTargetPos)
    {
        //  Debug.Log("happens");
        GridNodes startNode = grid.NodeFromWorldPos(cStartPos);
        GridNodes targetNode = grid.NodeFromWorldPos(cTargetPos);

        List<GridNodes> openList = new List<GridNodes>();
        HashSet<GridNodes> closedList = new HashSet<GridNodes>();

        openList.Add(startNode);

        int counter = 0;

        finishedPath = false;

        while (openList.Count > 0)
        {
            GridNodes current = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < current.FCost || openList[i].FCost == current.FCost && openList[i].distFromEnd < current.distFromEnd)
                {
                    current = openList[i];
                    //Debug.Log(i);
                }
            }

            counter++;

            openList.Remove(current);
            closedList.Add(current);

            if (current == targetNode)
            {
                getFoundPath(startNode, targetNode);
                finishedPath = true;
                //Debug.Log("path found: " + counter);
                break;
            }

            foreach (GridNodes Neighbor in grid.GetNeighbours(current))
            {
                if (!Neighbor.isObstructed || closedList.Contains(Neighbor))
                {
                    continue;
                }

                int moveCost = current.distFromStart + ManhattenDist(current, Neighbor);

                if (!openList.Contains(Neighbor) || moveCost < Neighbor.FCost)
                {
                    Neighbor.distFromStart = moveCost;
                    Neighbor.distFromEnd = ManhattenDist(Neighbor, targetNode);
                    Neighbor.Parent = current;
                    
                    if (!openList.Contains(Neighbor))
                    {
                        openList.Add(Neighbor);
                    }
                }
                
            }
        }

    }

    void getFoundPath(GridNodes cStartNode, GridNodes cEndNode)
    {
        List<GridNodes> foundPath = new List<GridNodes>();
        List<Vector3> v3foundPath = new List<Vector3>();
        GridNodes current = cEndNode;

        while (current != cStartNode)
        {
            foundPath.Add(current);
            v3foundPath.Add(current.pos);
            current = current.Parent;
        }

        foundPath.Reverse();
        v3foundPath.Reverse();
        grid.foundPath = foundPath;
        storedPath = v3foundPath;
    }

    int ManhattenDist(GridNodes nodeA, GridNodes nobeB)
    {
        int ix = Mathf.Abs(nodeA.gridPosX - nobeB.gridPosX);
        int iy = Mathf.Abs(nodeA.gridPosY - nobeB.gridPosY);

        return ix + iy;
    }

}
