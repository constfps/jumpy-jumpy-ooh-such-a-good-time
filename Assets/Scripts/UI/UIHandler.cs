using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private Transform mainMenu;
    private Transform pauseMenu;
    private Transform optionsMenu;
    private Transform lastMenu;

    private Slider musicVolume;
    private Slider sfxVolume;
    private Canvas canvas;

    private CamHandler camHandler;
    private InputHandling inputHandling;
    private GunHandler gunHandler;
    private SfxManager sfxHandler;

    public bool paused = false;
    public bool canPause = false;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        mainMenu = canvas.transform.GetChild(1);
        pauseMenu = canvas.transform.GetChild(2);
        optionsMenu = canvas.transform.GetChild(3);

        musicVolume = optionsMenu.GetChild(0).GetChild(0).GetComponent<Slider>();
        sfxVolume = optionsMenu.GetChild(1).GetChild(0).GetComponent<Slider>();

        camHandler = FindObjectOfType<CamHandler>();
        inputHandling = FindObjectOfType<InputHandling>();
        sfxHandler = FindObjectOfType<SfxManager>();
        gunHandler = FindObjectOfType<GunHandler>();

        inputHandling.enabled = false;
        gunHandler.enabled = false;
    }
    
    public void StartGame()
    {
        inputHandling.enabled = true;
        gunHandler.enabled = true;
        mainMenu.gameObject.SetActive(false);
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        camHandler.SwitchCam();
        canPause = true;
    }

    public void Settings()
    {
        if (mainMenu.gameObject.activeSelf && !pauseMenu.gameObject.activeSelf)
        {
            lastMenu = mainMenu;
        }
        else if (!mainMenu.gameObject.activeSelf && pauseMenu.gameObject.activeSelf)
        {
            lastMenu = pauseMenu;
        }
        mainMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void Backshots()
    {
        if (lastMenu == pauseMenu)
        {
            pauseMenu.gameObject.SetActive(true);
        }
        else if (lastMenu == mainMenu)
        {
            mainMenu.gameObject.SetActive(true);
        }
        optionsMenu.gameObject.SetActive(false);
    }

    private void StupidShit()
    {
        Time.timeScale = 0f;
    }

    public void Pause()
    {
        if (canPause)
        {
            paused = true;
            canvas.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(true);
            camHandler.SwitchCam();
            Invoke("StupidShit", .25f);
        }
    }

    public void Resume()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            camHandler.SwitchCam();
            canvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        sfxHandler.source.volume = sfxVolume.value;
        sfxHandler.music.volume = musicVolume.value;
    }
}
