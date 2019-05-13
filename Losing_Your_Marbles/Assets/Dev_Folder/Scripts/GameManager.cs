using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Range(0, 250)]
    public int enemy_cap;

    [Range(1, 50)]
    public int num_marbles;

    [Range(100, 255)]
    public int width;

    [Range(100, 255)]
    public int height;

    [Range(5, 10)]
    public int passageWidth;

    public string seed;

    public bool useRandomSeed;

    [Range(35, 55)]
    public int randomFillPercent;

    [Range(20, 100)]
    public int wallThresholdSize;

    [Range(20, 100)]
    public int roomThresholdSize;

    [Range(0, 3)]
    public int forest_density;

    public bool disableMinimap;

    public AudioSource buttonAudio;

    public void SetDefaultSettings() {
        this.width = 155;
        widthSlider.value = 155;
        widthText.text = 155.ToString();
        this.height = 155;
        heightSlider.value = 155;
        heightText.text = 155.ToString();
        this.num_marbles = 15;
        marbleCountSlider.value = 15;
        numMText.text = 15.ToString();
        this.enemy_cap = 125;
        enemyCapSlider.value = 125;
        capText.text = 125.ToString();
        this.passageWidth = 6;
        passageWidthSlider.value = 6;
        passagewidthText.text = 6.ToString();
        this.randomFillPercent = 48;
        randomFillSlider.value = 48;
        fillText.text = 48.ToString();
        this.wallThresholdSize = 50;
        wallThreshholdSlider.value = 50;
        wallTText.text = 50.ToString();
        this.roomThresholdSize = 50;
        roomThreshholdSlider.value = 50;
        roomTText.text = 50.ToString();
        this.forest_density = 2;
        forestDensitySlider.value = 2;
        FDText.text = 2.ToString();
        this.useRandomSeed = true;
        randomSeedToggle.isOn = true;
        this.seed = "Enter Seed...";
        seedInput.text = "Enter Seed...";
        this.disableMinimap = false;
        //miniMapToggle.isOn = false;
    }

    public Slider widthSlider;
    public Slider heightSlider;
    public Slider marbleCountSlider;
    public Slider enemyCapSlider;

    public Slider randomFillSlider;
    public Slider forestDensitySlider;

    public Slider passageWidthSlider;
    public Slider wallThreshholdSlider;
    public Slider roomThreshholdSlider;

    public Toggle randomSeedToggle;
    public InputField seedInput;

   // public Toggle miniMapToggle;

    public Button resetButton;

    public Button toSettingsButton;

    public Button backButton;

    public Button playButton;

    public Button quitButton;

    public Text widthText;
    public Text heightText;
    public Text numMText;
    public Text capText;
    public Text fillText;
    public Text FDText;
    public Text passagewidthText;
    public Text wallTText;
    public Text roomTText;

    //public Slider LoadingSlider;

    private GameObject MainMenu;
    private GameObject SettingsMenu;
    private GameObject LoadingMenu;


    private void Start()
    {

            widthText = GameObject.Find("WidthNum").GetComponent<Text>();
            heightText = GameObject.Find("HeightNum").GetComponent<Text>();
            numMText = GameObject.Find("NumMarblesText").GetComponent<Text>();
            capText = GameObject.Find("EnemyCap").GetComponent<Text>();
            fillText = GameObject.Find("RFPTtext").GetComponent<Text>();
            FDText = GameObject.Find("ForestDensityText").GetComponent<Text>();
            passagewidthText = GameObject.Find("PassageWidthText").GetComponent<Text>();
            wallTText = GameObject.Find("WallTText").GetComponent<Text>();
            roomTText = GameObject.Find("RoomTText").GetComponent<Text>();

        Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1;

            this.MainMenu = GameObject.Find("MainMenu");
            this.SettingsMenu = GameObject.Find("SettingsMenu");
            this.LoadingMenu = GameObject.Find("Loading");

            SetDefaultSettings();

            widthSlider.onValueChanged.AddListener(delegate { SetWidth((int)widthSlider.value); });
            heightSlider.onValueChanged.AddListener(delegate { SetHeigth((int)heightSlider.value); });
            marbleCountSlider.onValueChanged.AddListener(delegate { SetNumMarbles((int)marbleCountSlider.value); });
            enemyCapSlider.onValueChanged.AddListener(delegate { SetEnemyCap((int)enemyCapSlider.value); });

            randomFillSlider.onValueChanged.AddListener(delegate { SetRandomFillPercent((int)randomFillSlider.value); });
            forestDensitySlider.onValueChanged.AddListener(delegate { SetForestDensity((int)forestDensitySlider.value); });

            passageWidthSlider.onValueChanged.AddListener(delegate { SetPassageWidth((int)passageWidthSlider.value); });
            wallThreshholdSlider.onValueChanged.AddListener(delegate { SetWallThreshholdSize((int)wallThreshholdSlider.value); });
   
            roomThreshholdSlider.onValueChanged.AddListener(delegate { SetRoomThresholdSize((int)roomThreshholdSlider.value); });


            randomSeedToggle.onValueChanged.AddListener(delegate { SetUseRandomSeed(randomSeedToggle.isOn); });
            seedInput.onValueChanged.AddListener(delegate { SetSeed(seedInput.text); });
            //miniMapToggle.onValueChanged.AddListener(delegate { SetMapStatus(miniMapToggle.isOn); });

            resetButton.onClick.AddListener(delegate { SetDefaultSettings(); });
            toSettingsButton.onClick.AddListener(delegate { ToSettings(); });
            backButton.onClick.AddListener(delegate { BackButton(); });
            quitButton.onClick.AddListener(delegate { QuitButton(); });
            playButton.onClick.AddListener(delegate { PlayButtonPressed(); });

            this.SettingsMenu.SetActive(false);
            this.LoadingMenu.SetActive(false);

    }

    public void PlayButtonPressed()
    {
        StartCoroutine(StartAudio(buttonAudio));
        MapGenerator.enemy_cap = this.enemy_cap;
        MapGenerator.num_marbles = this.num_marbles;
        MapGenerator.width = this.width;
        MapGenerator.height = this.height;
        MapGenerator.passageWidth = this.passageWidth;
        MapGenerator.seed = this.seed;
        MapGenerator.useRandomSeed = this.useRandomSeed;
        MapGenerator.randomFillPercent = this.randomFillPercent;
        MapGenerator.wallThresholdSize = this.wallThresholdSize;
        MapGenerator.roomThresholdSize = this.roomThresholdSize;
        MapGenerator.forest_density = this.forest_density;
        MapGenerator.disableMinimap = this.disableMinimap;
        StartCoroutine("LoadYourAsyncScene");
    }

    IEnumerator LoadYourAsyncScene()
    {
        this.LoadingMenu.SetActive(true);
        this.MainMenu.SetActive(false);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        { 
            yield return null;
        }

    }
    public void ToSettings() {
        StartCoroutine(StartAudio(buttonAudio));
        this.MainMenu.SetActive(false);
        this.SettingsMenu.SetActive(true);
    }

    public void BackButton()
    {
        StartCoroutine(StartAudio(buttonAudio));
        this.MainMenu.SetActive(true);
        this.SettingsMenu.SetActive(false);
    }

    public void QuitButton() {
        StartCoroutine(StartAudio(buttonAudio));
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }

    public void SetWidth(int w)
    {
        StartCoroutine(StartAudio(buttonAudio));
        width = w;
        widthText.text = w.ToString();
    }

    public void SetHeigth(int h)
    {
        StartCoroutine(StartAudio(buttonAudio));
        height = h;
        heightText.text = h.ToString();
    }

    public void SetNumMarbles(int num)
    {
        StartCoroutine(StartAudio(buttonAudio));
        num_marbles = num;
        numMText.text = num.ToString();
    }

    public void SetEnemyCap(int cap)
    {
        StartCoroutine(StartAudio(buttonAudio));
        enemy_cap = cap;
        capText.text = cap.ToString();
    }

   
    public void SetPassageWidth(int pw)
    {
        StartCoroutine(StartAudio(buttonAudio));
        passageWidth = pw;
        passagewidthText.text = pw.ToString();
    }

    public void SetSeed(String s)
    {
        StartCoroutine(StartAudio(buttonAudio));
        StartCoroutine(StartAudio(buttonAudio));
        seed = s;
    }

    public void SetUseRandomSeed(bool b)
    {
        StartCoroutine(StartAudio(buttonAudio));
        useRandomSeed = b;

    }

    public void SetRandomFillPercent(int num)
    {
        StartCoroutine(StartAudio(buttonAudio));
        randomFillPercent = num;
        fillText.text = num.ToString();
    }

    public void SetWallThreshholdSize(int num)
    {
        StartCoroutine(StartAudio(buttonAudio));
        wallThresholdSize = num;
        wallTText.text = num.ToString();
    }

    public void SetRoomThresholdSize(int num)
    {
        StartCoroutine(StartAudio(buttonAudio));
        roomThresholdSize = num;
        roomTText.text = num.ToString();
    }

    public void SetForestDensity(int num)
    {
        StartCoroutine(StartAudio(buttonAudio));
        forest_density = num;
        FDText.text = num.ToString();
    }

    public void SetMapStatus(bool enabled)
    {
        StartCoroutine(StartAudio(buttonAudio));
        disableMinimap = enabled;

    }

    IEnumerator StartAudio(AudioSource source)
    {
        source.Play();
        yield return new WaitForSeconds(source.clip.length);

    }

}
