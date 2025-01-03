using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Movement movement;
    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if (rb.velocity.y > 0f && !movement.hasJump)
        {
            animator.SetBool("jumping", true);
        } 
        else if (rb.velocity.y < 0f && !movement.hasJump)
        {
            animator.SetBool("falling", true);
            animator.SetBool("jumping", false);
        }
        else if (movement.hasJump)
        {
            animator.SetBool("falling", false);
            animator.SetBool("jumping", false);
        }

        if ((facingRight && Input.GetAxisRaw("Horizontal") < 0f || !facingRight && Input.GetAxisRaw("Horizontal") > 0f) && movement.hasJump)
        {
            flip();
        }
    }

    private void OnDisable()
    {
        animator.SetBool("jumping", false);
        animator.SetBool("falling", false);
        animator.SetFloat("speed", 0f);
    }

    public void flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
        scale = transform.GetChild(2).localScale;
        scale.x *= -1f;
        transform.GetChild(2).localScale= scale;
    }
}
