using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6.0f;
    Animator animator;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Vector2 moveDirection = new Vector2(1, 0);

    private Inventory inventory;
    [SerializeField] private UI_Inventory ui_Inventory; 

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        inventory = new Inventory();
        ui_Inventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(10, 18), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(10, 24), new Item { itemType = Item.ItemType.Sword, amount = 1 });

        ItemWorld.SpawnItemWorld(new Vector3(5, 24), new Item { itemType = Item.ItemType.Sword, amount = 1 });

        ItemWorld.SpawnItemWorld(new Vector3(5, 18), new Item { itemType = Item.ItemType.Sword, amount = 1 });


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

        if (itemWorld != null)
        {
            //Touching item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    // Update is called once per frame
    void Update()
    {
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

    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;
    }
}
