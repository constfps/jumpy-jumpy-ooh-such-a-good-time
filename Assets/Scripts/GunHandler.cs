using UnityEngine;

public class GunHandler : MonoBehaviour
{
    private Camera cam;
    private Transform player;
    private Movement movement;
    private LineRenderer lineRenderer;
    private Vector3 tip;

    private Vector3 directionToPointer;
    private Vector3 mousePos;
    [SerializeField] public LayerMask launcherLayer;

    void Start()
    {
        cam = Camera.main; 
        player = transform.parent;
        movement = player.GetComponent<Movement>();
        lineRenderer = transform.parent.GetComponent<LineRenderer>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        directionToPointer = mousePos - transform.position;
        float rotZ = Mathf.Atan2(directionToPointer.y, directionToPointer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        transform.position = player.position;
    }

    void DrawLine(Vector3 hitPos)
    {
        tip = transform.parent.Find("gunTip").position;
        lineRenderer.SetPosition(0, hitPos);
        lineRenderer.SetPosition(1, tip);
    }

    public void Fire()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, directionToPointer, float.PositiveInfinity, launcherLayer);
        if (hit.transform.tag == "Launcher")
        {
            Debug.Log("Launcher detected");
            movement.launch(directionToPointer.normalized);
            DrawLine(hit.point);
        }
    }
}
