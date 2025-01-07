using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public Transform background;
    public Transform title;
    public Transform mainMenu;
    public Transform pauseMenu;
    public Transform optionsMenu;
    public Transform lastMenu;
    public Transform tutorial;
    public Transform stopwatch;
    public Transform endScreen;
    public Transform credits;

    private Slider musicVolume;
    private Slider sfxVolume;
    private Toggle tutToggle;
    private Toggle previewToggle;
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
    public bool skipPreview = true; //used to be exclusive to testing but made it an actual setting
    
    public GameObject arrows;
    public GameObject tutorials;
    public Transform endPos;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        //imports the entire UI hierarchy (check here first if shit breaks)
        background = canvas.transform.GetChild(0);
        title = canvas.transform.GetChild(1);
        mainMenu = canvas.transform.GetChild(2);
        pauseMenu = canvas.transform.GetChild(3);
        optionsMenu = canvas.transform.GetChild(4);
        bars = canvas.transform.GetChild(5);
        tutorial = canvas.transform.GetChild(6);
        stopwatch = canvas.transform.GetChild(7);
        endScreen = canvas.transform.GetChild(8);
        fade = canvas.transform.GetChild(9).GetComponent<Animator>();
        credits = canvas.transform.GetChild(10);

        musicVolume = optionsMenu.GetChild(0).GetChild(0).GetComponent<Slider>();
        sfxVolume = optionsMenu.GetChild(1).GetChild(0).GetComponent<Slider>();
        tutToggle = optionsMenu.GetChild(2).GetChild(0).GetComponent<Toggle>();
        previewToggle = optionsMenu.GetChild(3).GetChild(0).GetComponent<Toggle>();

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

    public void UIMassDisable(int[] exceptions)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!exceptions.Contains(i)) canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void UIMassDisable(int exception)
    {
        for (int i = 0; i < 9; i++)
        {
            if (exception != i) canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void QuickMainMenu()
    {
        credits.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
    }

    public void Credits()
    {
        credits.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
    }
    
    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(true);

        if (skipPreview)
        {
            canvas.transform.GetChild(9).GetComponent<LevelPreview>().StartGame();
        }
        else
        {
            fade.GetComponent<LevelPreview>().allowed = 0;
            fade.SetTrigger("fade out");
        }
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
            Stopwatch.stopStopwatch();
            camHandler.SwitchCam(0);
            Invoke("AntiDesync", .25f);
        }
    }

    public void Resume()
    {
        if (paused)
        {
            Stopwatch.startStopwatch();
            paused = false;
            Time.timeScale = 1f;
            pauseMenu.gameObject.SetActive(false);
            background.GetComponent<Animator>().SetTrigger("turn off");
            camHandler.SwitchCam(1);
            massEnable();
        }
    }

    public void MainMenu()
    {
        fade.GetComponent<LevelPreview>().allowed = 1;
        Time.timeScale = 1f;
        paused = false;
        canPause = false;
        triggerHandler.transform.GetComponent<DeathHandling>().respawnPos = GetComponent<Reset>().origin;
        massDisable();
        fade.SetTrigger("fade out");
    }

    public void EndScreen()
    {
        fade.GetComponent<LevelPreview>().allowed = 2;
        fade.SetTrigger("fade out");
        paused = false;
        canPause = false;
    }

    public void UpdateEndScreenText()
    {
        TMP_Text finalTimeText = endScreen.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text bestTimeText = endScreen.GetChild(1).GetComponent<TMP_Text>();
        finalTimeText.text = "Final Time: " + Stopwatch.finalTime.ToString(@"mm\:ss\:fff");
        bestTimeText.text = "Best Time: " + Stopwatch.bestTime.ToString(@"mm\:ss\:fff");
    }

    private void Update()
    {
        sfxHandler.source.volume = sfxVolume.value;
        sfxHandler.music.volume = musicVolume.value;
        tutEnabled = tutToggle.isOn;
        skipPreview = previewToggle.isOn;
    }
}
