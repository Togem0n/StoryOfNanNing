using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Transform originParent;
    private Transform originGrandpa;
    private Vector3 originPosition;
    private int originIndex;
    private Inventory inventory;
    private List<Item> itemList;
    [SerializeField] private PlayerController player;


    TextMeshProUGUI originText;


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
        originGrandpa = transform.parent.parent;
        originParent = transform.parent;
        originPosition = transform.position;
        string[] originParentName = transform.parent.name.ToString().Split('_');
        originIndex = int.Parse(originParentName[0]);
        originText = originParent.Find("text").GetComponent<TextMeshProUGUI>();

        originText.color = new Color32(255, 255, 225, 0);
        //Debug.Log(originIndex);

        transform.position = eventData.position;
        transform.SetParent(transform.parent.parent);

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;   
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);

        if(eventData.pointerCurrentRaycast.gameObject.name == "image")
        {

            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            //Save itemList
            Transform nextParent = eventData.pointerCurrentRaycast.gameObject.transform.parent;
            string[] nextParentName = nextParent.name.ToString().Split('_');
            int nextIndex = int.Parse(nextParentName[0]);
            //Debug.Log(nextIndex);

            Item nextItem = itemList[nextIndex];
            Item originItem = itemList[originIndex];
            originItem.index = nextIndex;
            nextItem.index = originIndex;

            itemList.RemoveAt(nextIndex);
            itemList.Insert(nextIndex, originItem);
            itemList.RemoveAt(originIndex);
            itemList.Insert(originIndex, nextItem);

            // after itemList changed
            originParent.name = originIndex.ToString() + "_" + itemList[originIndex].itemType.ToString() + "_" + itemList[originIndex].amount.ToString();
            nextParent.name = nextIndex.ToString() + "_" + itemList[nextIndex].itemType.ToString() + "_" + itemList[nextIndex].amount.ToString();

            TextMeshProUGUI nextText = nextParent.Find("text").GetComponent<TextMeshProUGUI>();
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
            
            //Save itemList
            eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originParent);
            eventData.pointerCurrentRaycast.gameObject.transform.position = originPosition;

            /* foreach (Item item in itemList)
            {
                Debug.Log(item.itemType.ToString() + " " + item.index.ToString());
            }*/


        }
        else
        {
            originText.color = new Color32(255, 255, 225, 255);
            transform.SetParent(originParent);
            transform.position = originPosition;
        }

        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
