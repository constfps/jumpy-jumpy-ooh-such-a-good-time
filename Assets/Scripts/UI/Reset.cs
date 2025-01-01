using UnityEngine;

public class Reset : MonoBehaviour
{
    public Transform player;
    public Transform origin;

    public void ResetPlayer()
    {
        player.position = origin.position;
    }
}
