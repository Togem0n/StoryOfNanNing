using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该脚本挂在于世界上所有的item底下
//底下有两个属性，一个是它的item，一个是它被拾取后要去的背包（逻辑有问题）
public class ItemOnWorld : MonoBehaviour
{
    public Item thisitem;
    public Inventory myBag;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !thisitem.canBePicked)
        {
            MyBagAddNewItem();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && thisitem.canBePicked)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                MyBagAddNewItem();
                Destroy(gameObject);
            }
        }

    }

    public void MyBagAddNewItem()
    {
        if (!myBag.itemList.Contains(thisitem))
        {
            //playerInventory.itemList.Add(thisitem);
            for (int i = 0; i < myBag.itemList.Count; i++)
            {
                if (myBag.itemList[i] == null)
                {
                    myBag.itemList[i] = thisitem;
                    break;
                }
            }
        }
        else
        {
            thisitem.itemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
}