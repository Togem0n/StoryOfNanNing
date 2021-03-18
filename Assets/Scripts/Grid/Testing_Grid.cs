using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing_Grid : MonoBehaviour
{

    private Grid grid;

    private void Start()
    {
        Debug.Log("nmsl");
        grid = new Grid(50, 50, 1f, new Vector3(0, 0));
        Debug.Log("nmsl");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}