using UnityEngine;

public class UIGunHandler : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;

    private Transform[] guns = new Transform[4];
    private Vector3[] directionToPointer = new Vector3[4];
    private LineRenderer[] lines = new LineRenderer[4];
    private Transform[] tips = new Transform[4];

    private void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i] = transform.GetChild(i);
        }
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = guns[i].GetComponent<LineRenderer>();
        }
        for (int i = 0; i < tips.Length; i++)
        {
            tips[i] = guns[i].GetChild(0);
        }
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            DrawLine(mousePos);
        }
        for (int i = 0; i < guns.Length; i++)
        {
            directionToPointer[i] = mousePos - guns[i].position;
            float rotZ = Mathf.Atan2(directionToPointer[i].y, directionToPointer[i].x) * Mathf.Rad2Deg;
            guns[i].rotation = Quaternion.Euler(0, 0, rotZ);
            if (rotZ >= 90f ||  rotZ <= -90f)
            {
                Vector3 scale = guns[i].localScale;
                scale.y = -10f;
                guns[i].localScale = scale;
            }
            else
            {
                Vector3 scale = guns[i].localScale;
                scale.y = 10f;
                guns[i].localScale = scale;
            }
        }
    }

    private void DrawLine(Vector3 hitPos)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].SetPosition(0, hitPos);
            lines[i].SetPosition(1, tips[i].position);
        }
    }
}
