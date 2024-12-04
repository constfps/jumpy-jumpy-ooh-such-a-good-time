using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;

    public bool hasJump = false;
    public bool canMove = true;

    public float jumpMultiplier = 5f;
    public float movementMultiplier = 5f;
    public float maxSpeed = 8f;

    public bool isWalled = false;
    public float wallslideSpeed = 2f;

    public RaycastHit2D hit;
    public float launcherMultiplier = 800f;
    public bool canLaunch = false;
    public bool launched = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (hasJump && !isWalled)
        {
            rb.velocity = new Vector2(0f, jumpMultiplier);
            hasJump = false;
        }
        else if (hasJump && isWalled)
        {
            rb.velocity = new Vector2(movementMultiplier * transform.localScale.x, jumpMultiplier);
        }
    }

    public void HorizontalMovement(float dir)
    {
        if (canMove && hasJump)
        {
            rb.velocity = new Vector2(dir * movementMultiplier, rb.velocity.y);
        }
        else if (canMove && !hasJump)
        {
            rb.velocity = rb.velocity;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contactPoint = new ContactPoint2D[collision.contactCount];
        int count = collision.GetContacts(contactPoint);

        //go through every contact point
        for (int i = 0; i < count; i++)
        {
            // ground check
            if (!hasJump && collision.collider.tag == "Ground" && contactPoint[i].normal.y > 0f)
            {
                hasJump = true;
                canMove = true;
                canLaunch = true;
                launched = false;
            }

            // Trampoline checker
            if (collision.collider.tag == "Trampoline" && contactPoint[i].normal.y > 0f)
            {
                hasJump = true;
                canMove = true;
                launched = false;
                canLaunch = true;
                Jump();
            }

            // walled checker
            if (collision.collider.tag == "Walljump" && contactPoint[i].normal.x != 0f)
            {
                isWalled = true;
                launched = false;
                hasJump = true;
                canMove = true;
                canLaunch = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isWalled && collision.collider.tag == "Walljump")
        {
            isWalled = false;
            hasJump = false;
        }

        if (collision.collider.tag == "Ground")
        {
            hasJump = false;
            canMove = false;
        }
    }

    public void wallslideHandler()
    {
        if (isWalled)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslideSpeed, float.MaxValue));
        }
    }

    public void launch(Vector2 dir)
    {
        if (!hasJump && canLaunch)
        {
            canMove = true;
            launched = true;
            canLaunch = false;
            rb.AddForce(-dir * launcherMultiplier, ForceMode2D.Impulse);
        }
    }
}
