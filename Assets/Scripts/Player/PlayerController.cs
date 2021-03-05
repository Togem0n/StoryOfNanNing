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

    // MyBag
    public GameObject myBag;
    bool isOpen;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        openMyBag();
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;
    }

    void openMyBag()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
            Tooltips.hideTooltips_Static();
        }
    }
}
