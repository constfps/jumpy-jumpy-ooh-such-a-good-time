using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private DeathHandling deathHandler;
    private SfxManager sfxManager;
    private UIHandler uiHandler;

    private void Start()
    {
        deathHandler = GetComponent<DeathHandling>();
        sfxManager = GetComponent<SfxManager>();
        uiHandler = FindObjectOfType<UIHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Checkpoint") && collision.transform.GetChild(0) != deathHandler.respawnPos)
        {
            deathHandler.respawnPos = collision.transform.GetChild(0);
            sfxManager.playCheckpoint();
        }

        if (collision.transform.CompareTag("End"))
        {
            Stopwatch.stopStopwatch();
            uiHandler.EndScreen();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Tutorial"))
        {
            Debug.Log("Tutorial triggered");
            uiHandler.changeTutImage(collision.transform.GetComponent<SpriteRenderer>().sprite);
            uiHandler.tutEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Tutorial") && UIHandler.tutEnabled)
        {
            uiHandler.tutExit();
        }
    }
}
