using UnityEngine;

public class LevelPreview : MonoBehaviour
{
    private CamHandler camHandler;
    private Animator fade;
    private UIHandler uiHandler;

    public int allowed = 0;

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
        if (allowed == 0)
        {
            camHandler.SwitchCam(2);
            fade.SetTrigger("fade in");
            uiHandler.bars.gameObject.SetActive(true);
            Invoke("goToEnd", 1);
        }
        else if (allowed == 1)
        {
            uiHandler.transform.GetComponent<Reset>().ResetPlayer();
            uiHandler.title.gameObject.SetActive(true);
            uiHandler.mainMenu.gameObject.SetActive(true);
            uiHandler.pauseMenu.gameObject.SetActive(false);
            uiHandler.bars.gameObject.SetActive(false);
            uiHandler.stopwatch.gameObject.SetActive(false);
            uiHandler.tutorial.gameObject.SetActive(false);
            uiHandler.endScreen.gameObject.SetActive(false);
            uiHandler.background.GetComponent<Animator>().SetTrigger("turn off");
            uiHandler.fade.SetTrigger("fade in");
            Stopwatch.reset();
        }
        else if (allowed == 2)
        {
            camHandler.SwitchCam(0);
            uiHandler.transform.GetComponent<Reset>().ResetPlayer(uiHandler.endPos);
            UIHandler.massDisable();
            int[] temp = { 5, 9 };
            uiHandler.UIMassDisable(temp);
            Stopwatch.updateBestTime();
            uiHandler.UpdateEndScreenText();
            uiHandler.endScreen.gameObject.SetActive(true);
            uiHandler.fade.SetTrigger("fade in");
        }
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
