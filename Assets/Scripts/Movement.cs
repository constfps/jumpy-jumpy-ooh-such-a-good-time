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

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (hasJump && !isWalled)
        {
            rb.velocity = Vector2.up * jumpMultiplier;
        }
        else if (hasJump && isWalled)
        {
            rb.velocity = new Vector2(movementMultiplier * transform.localScale.x, jumpMultiplier);
        }
    }

    public void MoveDirection(float dir)
    {
        if (canMove)
        {
            if (hasJump)
            {
                rb.velocity = new Vector2(dir * movementMultiplier, rb.velocity.y);
            }
            else
            {
                //i have absolutely no idea why multiplying this by 20 and having maxSpeed set to 3 caps the velocity to 5
                //TODO: fix velocity.x getting stuck set to maxSpeed and replicate the snappy feeling like velocity mode
                rb.AddForce(Vector2.right * dir * movementMultiplier * 20f);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).normal;

        // ground check
        if (!hasJump && collision.collider.tag == "Ground" && contactPoint.y > 0f)
        {
            hasJump = true;
        }

        // Trampoline checker
        if (collision.collider.tag == "Trampoline" && contactPoint.y > 0f)
        {
            hasJump = true;
            Jump();
        }

        // walled checker
        if (collision.collider.tag == "Walljump" && contactPoint.normalized.x != 0f)
        {
            isWalled = true;
            hasJump = true;
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
        rb.AddForce(-dir * launcherMultiplier, ForceMode2D.Impulse);
    }
}
