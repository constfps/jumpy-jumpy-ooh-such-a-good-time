using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    private ParticleSystem runningParticle;
    private Movement movement;

    public bool running = false;

    private void Start()
    {
        runningParticle = transform.Find("RunningPS").GetComponent<ParticleSystem>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f && movement.hasJump && !running)
        {
            running = true;
            runningParticle.Play();
            Debug.Log("Should be playing");
        }
        else if ((Input.GetAxisRaw("Horizontal") == 0f && running) || !movement.hasJump)
        {
            running = false;
            runningParticle.Stop();
        }
    }
}
