using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    //[Header("General Variables")]

    [Header("UI Elements")]
    public GameObject tryAgainButton;
    public GameObject nextLevelButton;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        
    }

    public void TryAgainButtonPressed()
    {
        GameManager._instance.TryAgainButtonPressed();

    } // TryAgainButtonPressed()

    public void NextLevelButtonPressed()
    {
        GameManager._instance.NextLevelButtonPressed();

    } // NextLevelButtonPressed()

    public void TriggerLevelFailed()
    {
        tryAgainButton.SetActive(true);

    } // TriggerLevelFailed()

    public void TriggerLevelSuccesed()
    {
        nextLevelButton.SetActive(true);

    } // TriggerLevelSuccesed()




} // class
