using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Cell
{
    private int value;
    private bool isTaken;
    private bool isWatered;

    public Cell(int value)
    {
        this.value = value;
    }

    public int getValue()
    {
        return this.value;
    }
    public void SetValue(int value)
    {
        this.value = value;
    }

}
