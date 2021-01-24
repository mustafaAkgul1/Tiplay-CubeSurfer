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

    [Header("References")]
    public Camera mainCamera;

    [Header("Vibration")]
    public HapticTypes cubeCollectionHapticType;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            TriggerGameStarted();
        }

        HandleWinning();

    } // Update()

    void TriggerGameStarted()
    {
        //ui tutorial kapat
        PlayerMovementController._instance.TriggerGameStarted();

    } // TriggerGameStarted()

    void HandleWinning()
    {
        if (!isGameFinished && true) // win condition
        {
            isGameFinished = true;
            Debug.Log("Level Succesed");

            SetPlayerPrefSettings();
        }

    } // HandleWinning()

    void SetPlayerPrefSettings()
    {
        int tmpCurr = PlayerPrefs.GetInt("CurrentLevel");
        tmpCurr++;
        PlayerPrefs.SetInt("CurrentLevel", tmpCurr);

    } // SetPlayerPrefSettings()

    public void TriggerCubeCollect()
    {
        CameraController._instance.GetCollectedCubeCount();
        MMVibrationManager.Haptic(cubeCollectionHapticType);

    } // TriggerCubeCollect()

    public void TriggerCamFOV() // called when cube lost
    {
        CameraController._instance.GetCollectedCubeCount();

    } // DecreaseCamFOV()

    public void TriggerLevelFailed()
    {
        isGameFinished = true;
        UIManager._instance.TriggerLevelFailed();

    } // TriggerLevelFailed()



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
