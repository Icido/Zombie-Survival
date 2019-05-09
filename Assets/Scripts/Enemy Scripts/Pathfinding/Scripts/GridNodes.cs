using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNodes {


    public int gridPosX;
    public int gridPosY;

    public bool isObstructed;
    public Vector3 pos;

    public GridNodes Parent;

    public int distFromStart; //gCost
    public int distFromEnd; //hCost or Manhatten Distance


    public int FCost;

    void setFCost()
    {
        FCost = distFromStart + distFromEnd;
    }

    public GridNodes(bool cIsWall, Vector3 cPos, int cGridX, int cGridY)
    {
        isObstructed = cIsWall;
        pos = cPos;
        gridPosX = cGridX;
        gridPosY = cGridY;
    }
}


