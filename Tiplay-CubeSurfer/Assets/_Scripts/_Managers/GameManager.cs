using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("General Variables")]
    public bool isGameStarted = false;
    public bool isGameFinished = false;
    public float camFovChangeAmount;
    public float camFovChangeTime;
    public int gemAmount = 0;
    public int collectedGemAmount = 0;
    public bool isVibrationOn = true;

    [Header("References")]
    public Camera mainCamera;

    [Header("Vibration")]
    public HapticTypes cubeCollectionHapticType;
    public HapticTypes gemCollectionHapticType;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InitPrefValues();

    } // Start()

    void Update()
    {
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            TriggerGameStarted();
        }

    } // Update()

    void InitPrefValues()
    {
        DOTween.Clear(true);

        if (PlayerPrefs.HasKey("GemAmount"))
        {
            gemAmount = PlayerPrefs.GetInt("GemAmount");
        }

        if (PlayerPrefs.HasKey("VibrationOff"))
        {
            int tmp = PlayerPrefs.GetInt("VibrationOff");

            if (tmp == 0)
            {
                isVibrationOn = true;
            }
            else
            {
                isVibrationOn = false;
            }
        }

    } // InitPrefValues()

    void TriggerGameStarted()
    {
        PlayerMovementController._instance.TriggerGameStarted();
        UIManager._instance.TriggerCloseTutorialBar();

    } // TriggerGameStarted()

    void SetPlayerPrefSettings()
    {
        int tmpCurr = PlayerPrefs.GetInt("CurrentLevel");
        tmpCurr++;
        PlayerPrefs.SetInt("CurrentLevel", tmpCurr);

        PlayerPrefs.SetInt("GemAmount", gemAmount);

    } // SetPlayerPrefSettings()

    public void TriggerCubeCollect()
    {
        CameraController._instance.GetCollectedCubeCount();

        if (isVibrationOn)
        {
            MMVibrationManager.Haptic(cubeCollectionHapticType);
        }

    } // TriggerCubeCollect()

    public void TriggerGemCollectedHaptic()
    {
        if (isVibrationOn)
        {
            MMVibrationManager.Haptic(gemCollectionHapticType);
        }

    } // TriggerGemCollectedHaptic()

    public void TriggerCamFOV() // called when cube lost
    {
        CameraController._instance.GetCollectedCubeCount();

    } // DecreaseCamFOV()

    public void TriggerLevelFailed()
    {
        isGameFinished = true;
        UIManager._instance.TriggerLevelFailed();

    } // TriggerLevelFailed()

    public void TriggerLevelSuccessed(int _multiplier)
    {
        isGameFinished = true;
        TriggerLevelEndGemCalculation(_multiplier);
        SetPlayerPrefSettings();

    } // TriggerLevelSuccessed()

    public void IncreaseCollectedGemAmount()
    {
        collectedGemAmount++;
        TriggerGemCollectedHaptic();

    } // IncreaseCollectedGemAmount()

    public void TriggerLevelEndGemCalculation(int _multiplier)
    {
        int gemSum = collectedGemAmount * _multiplier;
        gemAmount += gemSum;
        UIManager._instance.IncreaseGemAmountText(gemSum);
        UIManager._instance.TriggerLevelEndCanvas(collectedGemAmount, _multiplier, gemSum);
        TriggerGemCollectedHaptic();

    } // TriggerLevelEndGemCalculation()


    //Button pressing methods

    public void TryAgainButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    } // TryAgainButtonPressed()

    public void NextLevelButtonPressed()
    {
        //Working on same scene, mapcreator so can handle level load.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    } // NextLevelButtonPressed()

} // class
