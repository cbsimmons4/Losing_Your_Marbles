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

    public void SetDefaultSettings() {
        this.width = 155;
        widthSlider.value = 155;
        this.height = 155;
        heightSlider.value = 155;
        this.num_marbles = 15;
        marbleCountSlider.value = 15;
        this.enemy_cap = 125;
        enemyCapSlider.value = 125;
        this.passageWidth = 6;
        passageWidthSlider.value = 6;
        this.randomFillPercent = 48;
        randomFillSlider.value = 48;
        this.wallThresholdSize = 50;
        wallThreshholdSlider.value = 50;
        this.roomThresholdSize = 50;
        roomThreshholdSlider.value = 50;
        this.forest_density = 2;
        forestDensitySlider.value = 2;
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

   //public Slider LoadingSlider;

    private GameObject MainMenu;
    private GameObject SettingsMenu;
    private GameObject LoadingMenu;




    private void Start()
    {
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
        this.MainMenu.SetActive(false);
        this.SettingsMenu.SetActive(true);
    }

    public void BackButton()
    {
        this.MainMenu.SetActive(true);
        this.SettingsMenu.SetActive(false);
    }

    public void QuitButton() {
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
        width = w;
    }

    public void SetHeigth(int h)
    {
        height = h;
    }

    public void SetNumMarbles(int num)
    {
        num_marbles = num;
    }

    public void SetEnemyCap(int cap)
    {
        enemy_cap = cap;
    }

   
    public void SetPassageWidth(int pw)
    {
        passageWidth = pw;
    }

    public void SetSeed(String s)
    {
        seed = s;
    }

    public void SetUseRandomSeed(bool b)
    {
        useRandomSeed = b;
    }

    public void SetRandomFillPercent(int num)
    {
        randomFillPercent = num;
    }

    public void SetWallThreshholdSize(int num)
    {
        wallThresholdSize = num;
    }

    public void SetRoomThresholdSize(int num)
    {
        roomThresholdSize = num;
    }

    public void SetForestDensity(int num)
    {
        forest_density = num;
    }

    public void SetMapStatus(bool enabled)
    {
        disableMinimap = enabled;

    }

}
