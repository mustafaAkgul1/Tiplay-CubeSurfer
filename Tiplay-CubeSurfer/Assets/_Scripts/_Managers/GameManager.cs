using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void IncreaseCamFOV()
    {
        //if (DOTween.IsTweening("CamFOVIncreaseTween"))
        //{
        //    DOTween.Kill("CamFOVIncreaseTween");
        //}

        //if (DOTween.IsTweening("CamFOVDecreaseTween"))
        //{
        //    DOTween.Kill("CamFOVDecreaseTween", true);
        //}

        //mainCamera.DOFieldOfView(mainCamera.fieldOfView + camFovChangeAmount, camFovChangeTime).SetId("CamFOVIncreaseTween");

        CameraController._instance.GetCollectedCubeCount();

    } // IncreaseCamFOV()

    public void DecreaseCamFOV()
    {
        //if (DOTween.IsTweening("CamFOVIncreaseTween"))
        //{
        //    DOTween.Kill("CamFOVIncreaseTween", true);
        //}

        //if (DOTween.IsTweening("CamFOVDecreaseTween"))
        //{
        //    DOTween.Kill("CamFOVDecreaseTween", true);
        //}

        //mainCamera.DOFieldOfView(mainCamera.fieldOfView - camFovChangeAmount, camFovChangeTime).SetId("CamFOVDecreaseTween");

        CameraController._instance.GetCollectedCubeCount();

    } // DecreaseCamFOV()






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
