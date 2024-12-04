using UnityEngine;

public class DeathHandling : MonoBehaviour
{
    public Transform respawnPos;
    public Transform parent;
    public Movement movement;
    public Rigidbody2D rb;
    public CapsuleCollider2D localCollider;
    public SpriteRenderer localRenderer;

    private void Start()
    {
        respawnPos = GameObject.Find("Respawn Point").transform;
        parent = transform.parent;
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        localCollider = GetComponent<CapsuleCollider2D>(); 
        localRenderer = GetComponent<SpriteRenderer>();
    }

    private void Respawn()
    {
        gameObject.transform.position = respawnPos.position;
        localCollider.enabled = true;
        localRenderer.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        movement.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Death")
        {
            movement.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            localCollider.enabled = false;
            localRenderer.enabled = false;
            Invoke("Respawn", 1f);
        }
    }
}
