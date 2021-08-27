using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // PlayerMovement
    public float speed;
    float horizontal;
    float vertical;
    Animator animator;
    Rigidbody2D rigidbody2d;
    Vector2 moveDirection = new Vector2(1, 0);

    // InventorySystem
    private Inventory inventory;
    [SerializeField] private UI_Inventory ui_Inventory;
    [SerializeField] private DragDrop dragDrop;
    bool ui_Inventory_status = false;
    private int numOfOwnItem = 0;
    private Item itemInUse;

    //DialogueSystem
    [SerializeField] private DialogueUI dialogueUI; 
    public DialogueUI DialogueUI => dialogueUI; // Normally we use DialogueUI to call the showDialogue
    public IIteractable Interactable { get; set; }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        inventory = new Inventory();
        ui_Inventory.SetInventory(inventory);
        ui_Inventory.SetPlayer(this);
        ui_Inventory.gameObject.SetActive(false);

        itemInUse = new Item { itemType = Item.ItemType.None, amount = 1 };
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<ItemWorld>() != null)
        {
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

            if (numOfOwnItem < inventory.GetMaxCapacity())
            {
                if (itemWorld != null)
                {
                    //Touching item
                    if (inventory.itemInList(itemWorld.GetItem()))
                    {
                        if (!itemWorld.GetItem().IsStackable())
                        {
                            numOfOwnItem += 1;
                        }
                    }
                    else
                    {
                        numOfOwnItem += 1;
                    }
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }
            }
            else if (numOfOwnItem == inventory.GetMaxCapacity())
            {
                if (inventory.itemInList(itemWorld.GetItem()))
                {
                    if (itemWorld.GetItem().IsStackable())
                    {
                        inventory.AddItem(itemWorld.GetItem());
                        itemWorld.DestroySelf();
                    }
                }
            }
        }
        
    }

    void Update()
    {
        if (dialogueUI.dialogueIsOpen) return;

        // PlayerMovement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // InventorySystem
        OpenMyBag();

        // DialogueSystem
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interactable?.Interact(this);
        }

    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(ui_Inventory_status == false )
            {
                ui_Inventory.gameObject.SetActive(true);
            }
            else
            {
                ui_Inventory.gameObject.SetActive(false);
            }
            ui_Inventory_status = !ui_Inventory_status;
        }
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public Item GetItemInUse()
    {
        return itemInUse;
    }

    public void SetItemInUse(Item item)
    {
        itemInUse = item;
    }


}
