using UnityEngine;

public class Reset : MonoBehaviour
{
    public Transform player;
    public Transform origin;

    public void ResetPlayer()
    {
        player.position = origin.position;
    }

    public void ResetPlayer(Transform pos)
    {
        player.position = pos.position;
    }
}
