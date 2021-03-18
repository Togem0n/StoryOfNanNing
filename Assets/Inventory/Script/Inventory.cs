using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有AddItem, RemoveItem, GetItem基本方法
/// </summary>

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private int maxCapacity = 27;

    public Inventory()
    {
        itemList = new List<Item>();

        for (int i = 0; i < maxCapacity; i++)
        {
            itemList.Add(new Item { itemType = Item.ItemType.None, amount = 1, index = i});
        }

/*        for (int i = 0; i < maxCapacity; i++)
        {
            Debug.Log(itemList[i].index);
        }*/

        Debug.Log("Inventory");
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += 1;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                foreach (Item inventoryItem in itemList)
                {
                    Item None_item = new Item { itemType = Item.ItemType.None, amount = 1 };
                    if (inventoryItem.itemType == None_item.itemType)
                    {           
                        item.amount = 1;
                        item.index = inventoryItem.index;
                        itemList.RemoveAt(inventoryItem.index);
                        itemList.Insert(item.index, item);
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (Item inventoryItem in itemList)
            {
                Item None_item = new Item { itemType = Item.ItemType.None, amount = 1 };
                if (inventoryItem.itemType == None_item.itemType)
                {
                    item.amount = 1;
                    item.index = inventoryItem.index;
                    itemList.RemoveAt(inventoryItem.index);
                    itemList.Insert(item.index, item);
                    break;
                }
            }
        }


        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    // 删除有问题，需要解决
    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= 1;
                    
                    Debug.Log("item"+item.amount+item.itemType);

                    itemInInventory = inventoryItem;
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                //itemList.Remove(itemInInventory);
                Item None_item = new Item { itemType = Item.ItemType.None, amount = 1, index = itemInInventory.index };
                itemList.RemoveAt(itemInInventory.index);
                itemList.Insert(None_item.index, None_item);
                //itemInInventory.itemType = None_item.itemType;

            }
        }
        else
        {
            //itemList.Remove(item);
            Item None_item = new Item { itemType = Item.ItemType.None, amount = 1, index = item.index};
            itemList.RemoveAt(item.index);
            itemList.Insert(None_item.index, None_item);
            //item.itemType = None_item.itemType;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public int GetMaxCapacity()
    {
        return maxCapacity;
    }

    public void SetMaxCapacity(int maxCapacity)
    {
        this.maxCapacity = maxCapacity;
    }

    public bool itemInList(Item item)
    {
        foreach(Item titem in itemList)
        {
            if(titem.itemType == item.itemType)
            {
                return true;
            }
        }
        return false;
    }

}
