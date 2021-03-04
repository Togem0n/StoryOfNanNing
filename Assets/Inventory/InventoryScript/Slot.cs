using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
/// 代码部分简要说明：
/// InventoryManager，ItemOnWorld，Slot三个脚本共同构成“物品拾取加入物品槽”
/// 
/// 
/// </summary>




//该脚本用于储存背包内每一个slot的信息
//每个slot都有属于他的slot信息和那一个image和slotnum（有几个该item）
public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemInSlot;
    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
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
