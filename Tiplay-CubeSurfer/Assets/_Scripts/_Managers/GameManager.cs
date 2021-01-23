using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("General Variables")]
    public bool isGameFinished = false;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {


    } // Start()

    void Update()
    {
        HandleWinning();

    } // Update()
  
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
