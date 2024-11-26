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
        movement = transform.parent.GetChild(0).GetComponent<Movement>();
        player = transform.parent.GetChild(0).transform;
        movement = player.GetComponent<Movement>();
    }

    void Update()
    {
        // rotation logic
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        directionToPointer = mousePos - transform.position;
        float rotZ = Mathf.Atan2(directionToPointer.y, directionToPointer.x) * Mathf.Rad2Deg;

        // flip if needed
        if (rotZ <= -90f || rotZ >= 90)
        {
            Vector3 scale = transform.GetChild(0).transform.localScale;
            scale.y = -1f;
            transform.GetChild(0).transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.GetChild(0).transform.localScale;
            scale.y = 1f;
            transform.GetChild(0).transform.localScale = scale;
        }

        // set rotation
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //stick gun to player
        transform.position = player.position;
    }

    public void Fire()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPointer, float.PositiveInfinity, launcherLayer);
        if (hit.transform != null)
        {
            Debug.Log("Launcher detected");
            movement.launch(directionToPointer.normalized);
        }
    }
}
