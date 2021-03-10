using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;

public class Testing_Tilemap : MonoBehaviour
{
    public Tilemap ground;
    public Tile newtile;
    public Grid grid;
    private GameObject player;
    Vector2Int MouseLocation;
    Vector2Int PlayerLocation;

    private void Start()
    {
        grid = new Grid(50, 50, 1f, new Vector3(0, 0));
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int tmp_x;
            int tmp_y;
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out tmp_x, out tmp_y);
            MouseLocation = new Vector2Int(tmp_x, tmp_y);
            //Debug.Log("Mouse Location:" + MouseLocation);

            player = GameObject.FindGameObjectWithTag("Player");
            grid.GetXY(player.transform.position, out tmp_x, out tmp_y);
            PlayerLocation = new Vector2Int(tmp_x, tmp_y);
            //Debug.Log("Player Location:" + PlayerLocation);

            if( Mathf.Abs(PlayerLocation.x - MouseLocation.x) <= 1 && Mathf.Abs(PlayerLocation.y - MouseLocation.y) <= 1)
            {
                Debug.Log("Can !!!");
                ground.SetTile(new Vector3Int(MouseLocation.x, MouseLocation.y, 0), null);
            }
            else
            {
                Debug.Log("Can't !!!");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

}
