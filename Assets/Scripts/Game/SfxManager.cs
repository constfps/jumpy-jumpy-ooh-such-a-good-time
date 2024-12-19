using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip death;
    public AudioClip fire;
    public AudioClip land;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playJump()
    {
        source.clip = jump;
        source.Play();
    }

    public void playDeath()
    {
        source.clip = death;
        source.Play();
    }

    public void playFire()
    {
        source.clip = fire;
        source.Play();
    }

    public void playLand()
    {
        source.clip = land;
        source.Play();
    }
}
