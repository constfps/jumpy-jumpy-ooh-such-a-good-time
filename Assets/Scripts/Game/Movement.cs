using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private ParticleHandler psHandler;
    private SfxManager sfxManager;

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

    public bool inLadder = false;
    public bool climbing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        psHandler = GetComponent<ParticleHandler>();
        sfxManager = GetComponent<SfxManager>();
    }

    public void Jump()
    {
        if (hasJump && !isWalled)
        {
            rb.velocity = new Vector2(0f, jumpMultiplier);
            hasJump = false;
            sfxManager.playJump();
        }
        else if (hasJump && isWalled)
        {
            rb.velocity = new Vector2(movementMultiplier * transform.localScale.x, jumpMultiplier);
            sfxManager.playJump();
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

    public void VerticalMovement(float dir)
    {
        if (inLadder && !climbing && dir > 0f)
        {
            climbing = true;
        }

        if (climbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, dir * movementMultiplier);
        } else
        {
            rb.gravityScale = 3f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            inLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            inLadder = false;
            climbing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contactPoint = new ContactPoint2D[collision.contactCount];
        int count = collision.GetContacts(contactPoint);
        for (int i = 0; i < count; i++)
        {
            if (collision.collider.CompareTag("Ground") && contactPoint[i].normal.y > 0f)
            {
                sfxManager.playLand();
            }
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
