using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 该脚本用于生成名为MyBag的itemList的数据库
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();

}
