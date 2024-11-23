using UnityEngine;

public class InputHandling : MonoBehaviour
{
    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        movement.MoveDirection(Input.GetAxisRaw("Horizontal"));
        movement.walljumpHandler();

        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
            movement.walljumpEnd();
        }
    }
}
