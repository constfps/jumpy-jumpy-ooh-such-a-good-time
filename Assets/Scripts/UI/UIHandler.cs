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
    private Animator fade;

    private CamHandler camHandler;

    private InputHandling inputHandling;
    private AnimationHandler animationHandler;
    private GunHandler gunHandler;
    private SfxManager sfxHandler;
    private ParticleHandler particleHandler;
    private Movement movement;

    public bool paused = false;
    public bool canPause = false;
    public bool autoMute = true; //the music is a bit annoying during testing lmao

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        //might break if hierarchy is changed
        mainMenu = canvas.transform.GetChild(1);
        pauseMenu = canvas.transform.GetChild(2);
        optionsMenu = canvas.transform.GetChild(3);
        fade = canvas.transform.GetChild(4).GetComponent<Animator>();

        musicVolume = optionsMenu.GetChild(0).GetChild(0).GetComponent<Slider>();
        sfxVolume = optionsMenu.GetChild(1).GetChild(0).GetComponent<Slider>();

        camHandler = FindObjectOfType<CamHandler>();
        inputHandling = FindObjectOfType<InputHandling>();
        sfxHandler = FindObjectOfType<SfxManager>();
        animationHandler = FindObjectOfType<AnimationHandler>();
        gunHandler = FindObjectOfType<GunHandler>();
        movement = FindObjectOfType<Movement>();
        particleHandler = FindObjectOfType<ParticleHandler>();

        massDisable();

        if (autoMute)
        {
            musicVolume.value = 0;
        }
    }

    public void massDisable()
    {
        movement.enabled = false;
        inputHandling.enabled = false;
        gunHandler.enabled = false;
        animationHandler.enabled = false;
        particleHandler.enabled = false;
    }

    public void massEnable()
    {
        movement.enabled = true;
        inputHandling.enabled = true;
        animationHandler.enabled = true;
        gunHandler.enabled = true;
        particleHandler.enabled = true;
    }
    
    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        fade.SetTrigger("fade out");
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
        massDisable();
    }

    public void Pause()
    {
        if (canPause)
        {
            paused = true;
            pauseMenu.gameObject.SetActive(true);
            camHandler.SwitchCam(0);
            Invoke("StupidShit", .25f);
        }
    }

    public void Resume()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            pauseMenu.gameObject.SetActive(false);
            camHandler.SwitchCam(1);
            massEnable();
        }
    }

    private void Update()
    {
        sfxHandler.source.volume = sfxVolume.value;
        sfxHandler.music.volume = musicVolume.value;
    }
}
