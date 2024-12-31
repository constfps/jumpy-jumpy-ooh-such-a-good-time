using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Transform background;
    private Transform title;
    private Transform mainMenu;
    private Transform pauseMenu;
    private Transform optionsMenu;
    private Transform lastMenu;
    private Transform tutorial;

    private Slider musicVolume;
    private Slider sfxVolume;
    private Toggle tutToggle;
    private Canvas canvas;

    public Animator fade;
    public Transform bars;

    private CamHandler camHandler;

    private static InputHandling inputHandling;
    private static AnimationHandler animationHandler;
    private static GunHandler gunHandler;
    private static SfxManager sfxHandler;
    private static ParticleHandler particleHandler;
    private static TriggerHandler triggerHandler;
    private static Movement movement;

    public bool paused = false;
    public bool canPause = false;
    public bool tutShown = false;
    public bool autoMute = true; //the music is a bit annoying during testing lmao

    public static bool tutEnabled;
    public bool inSettings;
    
    public GameObject arrows;
    public GameObject tutorials;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        //imports the entire UI hierarchy (check here first if shit breaks)
        background = canvas.transform.GetChild(0);
        title = canvas.transform.GetChild(1);
        mainMenu = canvas.transform.GetChild(2);
        pauseMenu = canvas.transform.GetChild(3);
        optionsMenu = canvas.transform.GetChild(4);
        fade = canvas.transform.GetChild(5).GetComponent<Animator>();
        bars = canvas.transform.GetChild(6);
        tutorial = canvas.transform.GetChild(7);

        musicVolume = optionsMenu.GetChild(0).GetChild(0).GetComponent<Slider>();
        sfxVolume = optionsMenu.GetChild(1).GetChild(0).GetComponent<Slider>();
        tutToggle = optionsMenu.GetChild(2).GetChild(0).GetComponent<Toggle>();

        //import player components for mass enabling and disabling during cutscenes
        camHandler = FindObjectOfType<CamHandler>();
        inputHandling = FindObjectOfType<InputHandling>();
        sfxHandler = FindObjectOfType<SfxManager>();
        animationHandler = FindObjectOfType<AnimationHandler>();
        gunHandler = FindObjectOfType<GunHandler>();
        movement = FindObjectOfType<Movement>();
        particleHandler = FindObjectOfType<ParticleHandler>();
        triggerHandler = FindObjectOfType<TriggerHandler>();

        arrows = GameObject.Find("arrows");
        tutorials = GameObject.Find("Tutorial Triggers");

        arrows.SetActive(false);
        tutorials.SetActive(false);

        massDisable();

        if (autoMute)
        {
            musicVolume.value = 0;
        }
    }

    public void changeTutImage(Sprite replacement)
    {
        if (!tutShown)
        {
            tutorial.GetChild(0).GetComponent<RawImage>().texture = replacement.texture;
            tutorial.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, replacement.rect.size.x);
            tutorial.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, replacement.rect.size.y);
        }
    }

    public void tutExit()
    {
        if (tutShown || !tutEnabled)
        {
            tutorial.GetComponent<Animator>().SetTrigger("slide out");
        }
    }

    public void tutEnter()
    {
        if (!tutShown && tutEnabled)
        {
            tutorial.GetComponent<Animator>().SetTrigger("slide in");
        }
    }

    public void midTutDisableFix()
    {
        if (tutShown && !tutToggle.isOn)
        {
            tutExit();
        }
    }

    public void toggleTutShown()
    {
        tutShown = !tutShown;
    }

    public static void massDisable()
    {
        movement.enabled = false;
        inputHandling.enabled = false;
        gunHandler.enabled = false;
        animationHandler.enabled = false;
        particleHandler.enabled = false;
        triggerHandler.enabled = false;
    }

    public static void massEnable()
    {
        movement.enabled = true;
        inputHandling.enabled = true;
        animationHandler.enabled = true;
        gunHandler.enabled = true;
        particleHandler.enabled = true;
        triggerHandler.enabled = true;
    }

    
    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
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
        inSettings = true;
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
        inSettings = false;
    }

    private void AntiDesync()
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
            background.GetComponent<Animator>().SetTrigger("turn on");
            camHandler.SwitchCam(0);
            Invoke("AntiDesync", .25f);
        }
    }

    public void Resume()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            pauseMenu.gameObject.SetActive(false);
            background.GetComponent<Animator>().SetTrigger("turn off");
            camHandler.SwitchCam(1);
            massEnable();
        }
    }

    private void Update()
    {
        sfxHandler.source.volume = sfxVolume.value;
        sfxHandler.music.volume = musicVolume.value;
        tutEnabled = tutToggle.isOn;
    }
}
