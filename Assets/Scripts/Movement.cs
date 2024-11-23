using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool hasJump = false;
    public bool canMove = true;

    public float jumpMultiplier = 5f;
    public float movementMultiplier = 5f;

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
        Vector2 contactPoint = collision.GetContact(0).point;

        // ground check
        if (!hasJump && collision.collider.tag == "Ground" && contactPoint.y < transform.position.y)
        {
            hasJump = true;
        }

        // Trampoline checker
        if (collision.collider.tag == "Trampoline" && contactPoint.y < transform.position.y)
        {
            hasJump = true;
            Jump();
        }
    }
}
