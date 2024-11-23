using UnityEngine;

public class InputHandling : MonoBehaviour
{
    private Movement movement;
    private GunHandler gunHandler;

    private void Start()
    {
        movement = GetComponent<Movement>();
        gunHandler = transform.parent.GetChild(1).GetComponent<GunHandler>();
    }

    void Update()
    {
        movement.MoveDirection(Input.GetAxisRaw("Horizontal"));
        movement.wallslideHandler();

        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            gunHandler.Fire();
        }
    }
}
