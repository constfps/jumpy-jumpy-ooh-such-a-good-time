using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private string walljumpDir = "left";

    public bool walljumping = false;
    public bool hasJump = false;
    public bool canMove = true;
    public float jumpMultiplier = 5f;
    public float movementMultiplier = 5f;
    public float walljumpGrav = 1f;
    public float normalGrav = 3f;
    public float walljumpHorizMultiplier = 5f;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (hasJump)
        {
            rb.velocity = Vector2.up * jumpMultiplier;
            hasJump = false;
        }
    }

    public void MoveDirection(float dir)
    {
        if (canMove){rb.velocity = new Vector2(dir * movementMultiplier, rb.velocity.y);}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ground check
        if (!hasJump && collision.collider.tag == "Ground" && collision.GetContact(0).point.y < transform.position.y)
        {
            hasJump = true;
        }

        // Trampoline checker
        if (collision.collider.tag == "Trampoline" && collision.GetContact(0).point.y < transform.position.y)
        {
            hasJump = true;
            Jump();
        }
        
        if ( 
            Mathf.Abs(transform.InverseTransformPoint(new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 0)).x) >
            Mathf.Abs(transform.InverseTransformPoint(new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 0)).y))
        {
            Debug.Log("Collided on either left or right");
        } else
        {
            Debug.Log("Collided on either top or bottom");
        }
    }

    public void walljumpEnd()
    {
        if (walljumping)
        {
            Jump();
            if (walljumpDir == "left")
            {
                rb.velocity += Vector2.left * walljumpHorizMultiplier;
            }
            else if (walljumpDir == "right")
            {
                rb.velocity += Vector2.right * walljumpHorizMultiplier;
            }

            walljumping = false;
            canMove = true;
        }
    }

    public void walljumpHandler()
    {
        if (walljumping)
        {
            canMove = false;
            rb.gravityScale = walljumpGrav;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        else
        {
            rb.gravityScale = normalGrav;
        }
    }
}
