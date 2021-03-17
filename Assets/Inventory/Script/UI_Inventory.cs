using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public PlayerController player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 53.5f;
        foreach(Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            /*itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //use
            };*/
            
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item None_item = new Item { itemType = Item.ItemType.None, amount = 1, index = -1};
                if(item.itemType != None_item.itemType)
                {
                    Debug.Log("Clicked");
                    inventory.RemoveItem(item);
                    ItemWorld.DropItem(player.GetPosition(), item);
                }
                //Debug.Log("Clicked");
                //inventory.RemoveItem(item);
                //ItemWorld.DropItem(player.GetPosition(), item);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize - 215f, y * itemSlotCellSize + 60f);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uitext = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();

            if(image.sprite == ItemAssets.Instance.NoneSprite)
            {
                image.color = new Color32(255, 255, 225, 0);
            }

            if (item.amount > 1)
            {
                uitext.SetText(item.amount.ToString());
            }
            else
            {
                uitext.SetText("");
            }

            x++;
            if(x >= 9)
            {
                x = 0;
                y--;
            }
        }
    }


}
