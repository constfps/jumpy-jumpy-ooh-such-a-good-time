using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private bool inLaunchTut;
    private bool inWallTut;

    private DeathHandling deathHandler;

    private void Start()
    {
        deathHandler = GetComponent<DeathHandling>();
    }

    private void Update()
    {
        if (inLaunchTut || inWallTut)
        {
            Time.timeScale = 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Checkpoint"))
        {
            deathHandler.respawnPos = collision.transform.GetChild(0);
        }

        if (collision.CompareTag("Launch Tutorial"))
        {
            inLaunchTut = true;
        }

        if (collision.CompareTag("Walljump Tutorial"))
        {
            inWallTut = true;
        }
    }
}
