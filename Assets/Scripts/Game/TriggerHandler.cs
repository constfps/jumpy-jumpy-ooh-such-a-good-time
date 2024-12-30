using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private bool inLaunchTut;
    private bool inWallTut;

    private DeathHandling deathHandler;
    private SfxManager sfxManager;

    private void Start()
    {
        deathHandler = GetComponent<DeathHandling>();
        sfxManager = GetComponent<SfxManager>();
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
        if (collision.transform.CompareTag("Checkpoint") && collision.transform.GetChild(0) != deathHandler.respawnPos)
        {
            deathHandler.respawnPos = collision.transform.GetChild(0);
            sfxManager.playCheckpoint();
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
