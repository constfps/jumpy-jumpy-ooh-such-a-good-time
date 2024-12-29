using UnityEngine;

public class LevelPreview : MonoBehaviour
{
    private CamHandler camHandler;
    private Animator fade;
    private UIHandler uiHandler;

    private void Start()
    {
        camHandler = FindObjectOfType<CamHandler>();
        fade = GetComponent<Animator>();
        uiHandler = FindObjectOfType<UIHandler>();
    }

    public void StartPreview()
    {
        camHandler.SwitchCam(2);
        fade.SetTrigger("fade in");
        Invoke("goToEnd", 1);
    }

    public void StartGame()
    {
        camHandler.SwitchCam(1);
        uiHandler.canPause = true;
        uiHandler.massEnable();
    }

    public void goToEnd()
    {
        camHandler.SwitchCam(3);
        Invoke("StartGame", 3);
    }
}
