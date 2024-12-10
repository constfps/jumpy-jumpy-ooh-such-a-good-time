using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    private ParticleSystem runningParticle;
    private Movement movement;

    private void Start()
    {
        runningParticle = transform.GetChild(2).GetComponent<ParticleSystem>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f && movement.hasJump)
        {
            runningParticle.Play();
        }
    }


}
