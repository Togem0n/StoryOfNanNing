using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该脚本用于生成Item的数据库
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea]
    public string itemInfo;

    public bool equip;
}
