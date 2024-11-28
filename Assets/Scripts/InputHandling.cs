using UnityEngine;

public class InputHandling : MonoBehaviour
{
    private Movement movement;
    private GunHandler gunHandler;
    public float axisInput;

    private void Start()
    {
        movement = GetComponent<Movement>();
        gunHandler = transform.parent.GetChild(1).GetComponent<GunHandler>();
    }

    private void Update()
    {
        axisInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            gunHandler.Fire();
        }
    }

    void FixedUpdate()
    {
        movement.MoveDirection(axisInput);
        movement.wallslideHandler();
    }
}
