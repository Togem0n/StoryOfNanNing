using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisitem;
    public Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisitem))
        {
            playerInventory.itemList.Add(thisitem);
        }
        else
        {
            thisitem.itemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
}
