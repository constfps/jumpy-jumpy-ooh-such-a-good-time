using UnityEngine;

public class GunHandler : MonoBehaviour
{
    private Camera cam;
    private Transform player;
    private Movement movement;
    private LineRenderer lineRenderer;
    private Vector3 tip;
    private SfxManager sfxManager;

    private Gradient gradient = new Gradient();
    private Vector3 directionToPointer;
    private Vector3 mousePos;
    private float timeForFade = 0f;
    public float fadeDuration = 0.5f;
    [SerializeField] public LayerMask launcherLayer;

    void Start()
    {
        cam = Camera.main; 
        player = transform.parent;
        movement = player.GetComponent<Movement>();
        lineRenderer = transform.parent.GetComponent<LineRenderer>();
        sfxManager = transform.parent.GetComponent<SfxManager>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        directionToPointer = mousePos - transform.position;
        float rotZ = Mathf.Atan2(directionToPointer.y, directionToPointer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        transform.position = player.position;
        BeamFadeHandler();
    }

    void BeamFadeHandler()
    {
        GradientColorKey[] colors = { new GradientColorKey(Color.white, 0f), new GradientColorKey(Color.white, 1f) };
        GradientAlphaKey[] alpha = { new GradientAlphaKey(Mathf.Lerp(1f, 0f, timeForFade / fadeDuration), 0f), new GradientAlphaKey(Mathf.Lerp(1f, 0f, timeForFade / fadeDuration), 1f)};
        gradient.SetKeys(colors, alpha);
        lineRenderer.colorGradient = gradient;
        timeForFade += Time.deltaTime;
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
        if (hit.transform != null && hit.transform.tag == "Launcher")
        {
            movement.launch(directionToPointer.normalized);
            timeForFade = 0f;
            DrawLine(hit.point);
            sfxManager.playFire();
        }
    }
}
