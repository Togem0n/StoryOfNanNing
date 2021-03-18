using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell
{

    public enum CellType
    {
        UnFarmedland,    //没耕过的地
        FarmedLand,  //耕过的地
        WateredLand,  //浇过水的耕地
        Grass,
        Water,
        Building,
        Wasteland
    }


    public CellType cellType;
    // Index in the array
    public int x;
    public int y;
    // 该区块是否可用
    public bool isAvailable;


    public Cell(CellType cellType, int x, int y, bool isAvailable)
    {
        this.cellType = cellType;
        this.x = x;
        this.y = y;
        this.isAvailable = isAvailable;
    }


    // 耕地
    public void PlowLand()
    {
        if (this.cellType == CellType.UnFarmedland && this.isAvailable == true)
        {
            this.cellType = CellType.FarmedLand;
        }
    }

    public int GetValue()
    {
        return (int)cellType;
    }

    public void SetValue(CellType cellType)
    {
        this.cellType = cellType;
    }



}
