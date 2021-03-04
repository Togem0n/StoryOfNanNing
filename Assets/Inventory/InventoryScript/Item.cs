using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int itemHeld;


    public bool canBeEquip;
    public bool canBePicked;
    public bool canBeEatan;

    [TextArea]
    public string itemInfo;
}
