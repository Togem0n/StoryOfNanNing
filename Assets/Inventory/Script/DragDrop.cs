using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{

    private Transform originParent;
    private Transform originGrandpa;
    private Vector3 originPosition;
    private int originIndex;
    private TextMeshProUGUI originText;

    private Transform nextParent;
    private int nextIndex;
    private TextMeshProUGUI nextText;

    private Inventory inventory;
    private List<Item> itemList;

    [SerializeField] private PlayerController player;

    private void Start()
    {
        inventory = player.GetInventory();
        itemList = player.GetInventory().GetItemList();
    }

    private void Update()
    {

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("drag!");
        originGrandpa = transform.parent.parent;
        originParent = transform.parent;
        originPosition = transform.position;

        string[] originParentName = transform.parent.name.ToString().Split('_');
        originIndex = int.Parse(originParentName[0]);

        originText = originParent.Find("text").GetComponent<TextMeshProUGUI>();
        originText.color = new Color32(255, 255, 225, 0);

        transform.position = eventData.position;
        transform.SetParent(transform.parent.parent);

        transform.GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerPressRaycast.gameObject.name);
        transform.position = eventData.position;   
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "image")
        {

            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

            //Save itemList
            nextParent = eventData.pointerCurrentRaycast.gameObject.transform.parent;
            string[] nextParentName = nextParent.name.ToString().Split('_');
            nextIndex = int.Parse(nextParentName[0]);

            Item nextItem = itemList[nextIndex];
            Item originItem = itemList[originIndex];

            originItem.index = nextIndex;
            nextItem.index = originIndex;

            itemList.RemoveAt(nextIndex);
            itemList.Insert(nextIndex, originItem);
            itemList.RemoveAt(originIndex);
            itemList.Insert(originIndex, nextItem);
            //Save itemList


            // After itemList Changed, rename template and show origin/next text
            originParent.name = originIndex.ToString() + "_" + itemList[originIndex].itemType.ToString() + "_" + itemList[originIndex].amount.ToString();
            nextParent.name = nextIndex.ToString() + "_" + itemList[nextIndex].itemType.ToString() + "_" + itemList[nextIndex].amount.ToString();

            nextText = nextParent.Find("text").GetComponent<TextMeshProUGUI>();
            Debug.Log(itemList[originIndex].amount);
            Debug.Log(itemList[nextIndex].amount);

            originText.color = new Color32(255, 255, 225, 255);
            if (itemList[originIndex].amount >= 2)
            {
                originText.SetText(itemList[originIndex].amount.ToString());
            }
            else
            {
                originText.SetText("");
            }

            if(itemList[nextIndex].amount >= 2)
            {
                nextText.SetText(itemList[nextIndex].amount.ToString());
            }
            else
            {
                nextText.SetText("");
            }

            eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originParent);
            eventData.pointerCurrentRaycast.gameObject.transform.position = originPosition;
        }
        else
        {
            originText.color = new Color32(255, 255, 225, 255);
            transform.SetParent(originParent);
            transform.position = originPosition;
        }

        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("right click");
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.parent.name.ToString());
            string[] nameSub = eventData.pointerCurrentRaycast.gameObject.transform.parent.name.ToString().Split('_');
            int indexOfItemInUse = int.Parse(nameSub[0]);
            //Debug.Log(nameSub[1].ToString());
            player.SetItemInUse(itemList[indexOfItemInUse]);
        }
        Debug.Log(player.GetItemInUse().itemType);
    }
}
