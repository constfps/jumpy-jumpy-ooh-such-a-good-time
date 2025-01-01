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

    public void enableTutorials()
    {
        uiHandler.tutorials.SetActive(true);
    }

    public void StartPreview()
    {
        camHandler.SwitchCam(2);
        fade.SetTrigger("fade in");
        uiHandler.bars.gameObject.SetActive(true);
        Invoke("goToEnd", 1);
    }

    public void StartGame()
    {
        camHandler.SwitchCam(1);
        uiHandler.bars.GetComponent<Animator>().enabled = true;
        uiHandler.canPause = true;
        UIHandler.massEnable();
        uiHandler.stopwatch.gameObject.SetActive(true);
        Stopwatch.startStopwatch();
        Invoke("enableTutorials", 0.5f);
    }

    public void goToEnd()
    {
        uiHandler.arrows.SetActive(true);
        camHandler.SwitchCam(3);
        Invoke("StartGame", 3);
    }
}
