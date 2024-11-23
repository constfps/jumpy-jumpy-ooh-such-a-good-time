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
        movement.wallslideHandler();

        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }

        //if (movement.isWalled)
        //{
        //    movement.rb.velocity = Vector2.left * 15f;
        //}
    }
}
