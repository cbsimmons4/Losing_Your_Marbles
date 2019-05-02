using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Range(0, 75)]
    public static int enemy_cap;

    [Range(1, 50)]
    public static int num_marbles;

    [Range(100, 255)]
    public static int width;

    [Range(100, 255)]
    public static int height;

    [Range(5, 10)]
    public static int passageWidth;

    public static string seed;

    public static bool useRandomSeed;

    [Range(35, 55)]
    public static int randomFillPercent;

    [Range(20, 100)]
    public static int wallThresholdSize;

    [Range(20, 100)]
    public static int roomThresholdSize;

    [Range(0, 3)]
    public static int forest_density;

    public static bool disableMinimap;

    public void SetDefault() {
        widthSlider.value = 155;
        heightSlider.value = 155;
        marbleCountSlider.value = 15;
        enemyCapSlider.value = 25;
        passageWidthSlider.value = 6;
        randomFillSlider.value = 48;
        wallThreshholdSlider.value = 50;
        roomThreshholdSlider.value = 50;
        forestDensitySlider.value = 2;
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
   

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            SetDefault();

            randomSeedToggle.onValueChanged.AddListener(delegate { SetUseRandomSeed(randomSeedToggle.isOn); });
            widthSlider.onValueChanged.AddListener(delegate { SetWidth((int)widthSlider.value); });
            heightSlider.onValueChanged.AddListener(delegate { SetHeigth((int)heightSlider.value); });
            marbleCountSlider.onValueChanged.AddListener(delegate { SetNumMarbles((int)marbleCountSlider.value); });
            enemyCapSlider.onValueChanged.AddListener(delegate { SetEnemyCap((int)enemyCapSlider.value); });

            randomFillSlider.onValueChanged.AddListener(delegate { SetRandomFillPercent((int)randomFillSlider.value); });
            forestDensitySlider.onValueChanged.AddListener(delegate { SetForestDensity((int)forestDensitySlider.value); });
        }
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
        disableMinimap = !enabled;

    }


}
