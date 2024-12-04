using UnityEngine;

public class GunHandler : MonoBehaviour
{
    private Camera cam;
    private Transform player;
    private Movement movement;

    private Vector3 directionToPointer;
    [SerializeField] public LayerMask launcherLayer;

    void Start()
    {
        cam = Camera.main; 
        player = transform.parent.GetChild(0).transform;
        movement = player.GetComponent<Movement>();
    }

    void Update()
    {
        // rotation logic
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        directionToPointer = mousePos - transform.position;
        float rotZ = Mathf.Atan2(directionToPointer.y, directionToPointer.x) * Mathf.Rad2Deg;

        // set rotation
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //stick gun to player
        transform.position = player.position;
    }

    public void Fire()
    {

    }
}
