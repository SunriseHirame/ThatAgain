using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMockupController : MonoBehaviour
{
    [SerializeField]
    bool playerOne;

    [SerializeField] private float jumpForce;

    [SerializeField] private float speed;

    private bool onFloor;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool moved = false;
        if (playerOne)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Move(-1);
                moved = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                Move(1);
                moved = true;
            }

            if (Input.GetKey(KeyCode.W) && onFloor)
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(-1);
                moved = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(1);
                moved = true;
            }

            if (Input.GetKey(KeyCode.UpArrow) && onFloor)
            {
                Jump();
            }
        }

        if (!moved)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        onFloor = false;
    }

    private void Move(float dir)
    {
        rb.velocity = new Vector2(dir*speed,rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Vector2.Dot((other.GetContact(0).point - rb.position).normalized, Vector2.down) > 0.8)
        {
            onFloor = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (Vector2.Dot((other.GetContact(0).point - rb.position).normalized, Vector2.down) > 0.8)
        {
            onFloor = true;
        }
    }
}
