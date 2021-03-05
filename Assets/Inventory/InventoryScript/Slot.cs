using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// UI 部分简要说明
/// 初始化一个底板，也就是pannel，纯色底板，我们命名为 box
/// 该box底下含有Title(美丽旗舰店)Grid(一个个小方格背板,slot就存在grid底下)ItemDescription， UseBtn
/// UI 部分简要说明
/// 
/// Slot 部分简要说明
/// Slot也就是物品槽，含有的属性包括Item(是一个按钮，因为我们要按它触发该物品的描述)
/// Item按钮底下包括ItemImage和ItemNum
/// Slot 部分简要说明
/// 
/// Tooltips部分
/// 有一个小bug，原理是鼠标在slot上面则显示tooltips
/// 但是如果鼠标在slot上面时候，就按o键，那么tooltip则会一直显示。
/// 解决办法暂且通过按o也设置tooltip negative
/// 
/// </summary>

//该脚本用于储存背包内每一个slot的信息
//每个slot都有属于他的slot信息和那一个image和slotnum（有几个该item）
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotID;//物品槽id
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemInSlot;


    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Cursor Entering " + name + " GameObject");
        Tooltips.showTooltips_Static(slotInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Cursor Exiting " + name + " GameObject");
        Tooltips.hideTooltips_Static();
    }

    public void SetUpSlot(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemSprite;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }

}
