using Cinemachine;
using UnityEngine;

public class CamHandler : MonoBehaviour
{
    private float mainCamP;
    private float menuCamP;
    private CinemachineVirtualCamera mainCam;
    private CinemachineVirtualCamera menuCam;

    private void Start()
    {
        mainCam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        menuCam = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
        mainCamP = mainCam.Priority;
        menuCamP = menuCam.Priority;
    }

    public void SwitchCam()
    {
        float temp;
        if (mainCamP > menuCamP)
        {
            temp = mainCamP;
            mainCamP = menuCamP;
            menuCamP = temp;
        }
        else
        {
            temp = menuCamP;
            menuCamP = mainCamP;
            mainCamP = temp;
        }

        mainCam.Priority = (int)mainCamP;
        menuCam.Priority = (int)menuCamP;
    }
}
