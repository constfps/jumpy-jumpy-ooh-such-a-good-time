using Cinemachine;
using UnityEngine;

public class CamHandler : MonoBehaviour
{
    private int mainCamP;
    private int menuCamP;
    private int endCamP;
    private int startCamP;

    private CinemachineVirtualCamera mainCam;
    private CinemachineVirtualCamera menuCam;
    private CinemachineVirtualCamera endCam;
    private CinemachineVirtualCamera startCam;

    private void Start()
    {
        mainCam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        menuCam = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
        endCam = transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
        startCam = transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();

        mainCamP = mainCam.Priority;
        menuCamP = menuCam.Priority;
        endCamP = endCam.Priority;
        startCamP = startCam.Priority;
    }

    public void SwitchCam(int cam)
    {
        mainCamP = menuCamP = endCamP = 10;
        switch(cam)
        {
            case 0:
                mainCamP = 11;
                break;
            case 1:
                menuCamP = 11;
                break;
            case 2:
                endCamP = 11;
                break;
            case 3:
                startCamP = 11;
                break;
        }

        mainCam.Priority = mainCamP;
        menuCam.Priority = menuCamP;
        endCam.Priority = endCamP;
        startCam.Priority = startCamP;
    }
}
