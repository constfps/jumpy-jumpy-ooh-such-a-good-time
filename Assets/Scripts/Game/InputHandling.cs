using UnityEngine;

public class InputHandling : MonoBehaviour
{
    private Movement movement;
    private GunHandler gunHandler;
    public float haxisInput;
    public float vaxisInput;

    private void Start()
    {
        movement = GetComponent<Movement>();
        gunHandler = GetComponentInChildren<GunHandler>();
    }

    private void Update()
    {
        haxisInput = Input.GetAxisRaw("Horizontal");
        vaxisInput = Input.GetAxisRaw("Vertical");

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
        movement.HorizontalMovement(haxisInput);
        movement.VerticalMovement(vaxisInput);
        movement.wallslideHandler();
    }
}
