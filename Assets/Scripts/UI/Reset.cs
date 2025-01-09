using UnityEngine;

public class Reset : MonoBehaviour
{
    public Transform player;
    public Transform origin;

    public void ResetPlayer()
    {
        player.position = origin.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void ResetPlayer(Transform pos)
    {
        player.position = pos.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
