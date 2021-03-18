using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;

public class Testing_Grid : MonoBehaviour
{
    public Tilemap ground1;
    public Tile unFarmedlandTile;
    public Tile farmedlandTile;
    public Tile wateredlandTile;
    public Tile grassTile;
    public Tile buildingTile;
    public Tile wasteLandTile;


    private Grid grid;
    public Cell cell;
    public Item itemInUse;
    public int mouseX, mouseY;
    public int playerX, playerY;
    [SerializeField] private PlayerController player;
    

    private void Start()
    {
        //初始化整个地图grids（全都是草地）
        grid = new Grid(50, 50, 1f, new Vector3(0, 0));

        //把固定范围的地变成田地
        for (int x = 2;  x <= 12; x++ )
        {
            for (int y = 16; y <= 25; y++)
            {
                grid.SetValue(x, y, (int)Cell.CellType.UnFarmedland);
            }
        }

        itemInUse = player.GetItemInUse();


    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlowLand();
            WaterLand();
        }

        // 右键显示该区块的值和位置
        if (Input.GetMouseButtonDown(1))
        {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out mouseX, out mouseY);
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition())+" ("+mouseX+", "+mouseY+")");
        }
    }

    public void PlowLand()
    {
        // get item in use
        itemInUse = player.GetItemInUse();

        if (itemInUse.itemType == Item.ItemType.Hoe && grid.GetValue(UtilsClass.GetMouseWorldPosition()) == 0 && IsReachable())
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
            ground1.SetTile(new Vector3Int(mouseX, mouseY, 0), farmedlandTile);
        }
    }

    public void WaterLand()
    {
        // get item in use
        itemInUse = player.GetItemInUse();

        if (itemInUse.itemType == Item.ItemType.WaterBucket && grid.GetValue(UtilsClass.GetMouseWorldPosition()) == 1 && IsReachable())
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 2);
            ground1.SetTile(new Vector3Int(mouseX, mouseY, 0), wateredlandTile);
        }
    }

    public bool IsReachable()
    {
        // get the postions
        grid.GetXY(UtilsClass.GetMouseWorldPosition(), out mouseX, out mouseY);
        grid.GetXY(player.transform.position, out playerX, out playerY);

        if (Mathf.Abs(mouseX - playerX) <= 1 && Mathf.Abs(mouseY - playerY) <= 1)
        {
            return true;
        }

        return false;

    }



}