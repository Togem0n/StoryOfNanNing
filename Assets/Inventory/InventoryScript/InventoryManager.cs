using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InventoryManager挂在Canvas底下
/// 物品被拾取后执行函数顺序'ItemOnWorld/OnTriggerEnter2D'->'ItemOnWorld/AddNewItem'->'InventoryManager/RefreshItem'
/// 逻辑顺序是   
///             开始游戏时，InventoryManager初始化背包，在Grid底下生成18个slot->
///             物品被触碰拾取然后消失->
///             物品被加入playerInventory(仅进入数组，UI层面没显示)->
///             InventoryManager
/// </summary>

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory myBag;
    public GameObject slotGrid;
    // public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemInformation;
    public List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }

    /*public static void CreateNewItem(Item item)
    {
        //当我们碰撞item时候执行该方法CreateNewItem
        //然后此时在背包里生成slot存储该item 
   
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        //由于Instantiate是在Grid底下，此时我们设置他的parent
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        // newItem.slotImage是image，而Item.itemImage是一个sprite，所以前面要加.sprite获取它的sprite
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }
    */

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            //CreateNewItem(instance.myBag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);

            instance.slots[i].GetComponent<Slot>().slotID = i;

            instance.slots[i].GetComponent<Slot>().SetUpSlot(instance.myBag.itemList[i]);

        }
    }
}