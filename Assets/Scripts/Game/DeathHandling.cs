using UnityEngine;

public class DeathHandling : MonoBehaviour
{
    private Transform respawnPos;
    private Movement movement;
    private Rigidbody2D rb;
    private Collider2D localCollider;
    private SpriteRenderer localRenderer;
    private ParticleSystem deathFX;
    private SfxManager sfxManager;

    private void Start()
    {
        respawnPos = GameObject.Find("Respawn Point").transform;
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        localCollider = GetComponent<Collider2D>(); 
        localRenderer = GetComponent<SpriteRenderer>();
        deathFX = transform.Find("Death").GetComponent<ParticleSystem>();
        sfxManager = GetComponent<SfxManager>();
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
            sfxManager.playDeath();
            movement.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            deathFX.Play();
            localCollider.enabled = false;
            localRenderer.enabled = false;
            Invoke("Respawn", 1f);
        }
    }
}
