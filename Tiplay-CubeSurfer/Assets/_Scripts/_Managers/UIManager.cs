using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    [Header("References")]
    public Transform uiCanvasObject;

    [Header("UI Elements")]
    public GameObject tryAgainButton;
    public GameObject nextLevelButton;
    public GameObject popUpTextPrefab;
    public GameObject gemImage;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI gemAmountText;
    public Animator gemAreaAnimator;
    public GameObject levelEndCanvas;
    public TextMeshProUGUI gemMultiplierText;
    public TextMeshProUGUI collectedGemAmountText;
    public TextMeshProUGUI collectedGemSumAmountText;
    public GameObject tutorialSliderBarObject;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InitPrefValues();

    } // Start()

    void InitPrefValues()
    {
        if (PlayerPrefs.HasKey("GemAmount"))
        {
            gemAmountText.text = PlayerPrefs.GetInt("GemAmount").ToString();
        }

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevelText.text = PlayerPrefs.GetInt("CurrentLevel").ToString();
        }
        else
        {
            currentLevelText.text = "0";
        }

    } // InitPrefValues()

    public void CreatePopUpText(Transform _transform)
    {
        GameObject tmp = Instantiate(popUpTextPrefab, _transform.position + (Vector3.right * 1.5f), Quaternion.identity, _transform);
        Destroy(tmp, 0.65f);

    } // CreatePopUpText

    public void IncreaseGemAmountText(int _amount)
    {
        TriggerCoinReachedAnimation();

        int tmp = int.Parse(gemAmountText.text);
        tmp += _amount;
        gemAmountText.text = tmp.ToString();

        PlayerPrefs.SetInt("GemAmount", tmp);

    } // IncreaseCoinText()

    public void TriggerCoinReachedAnimation()
    {
        gemAreaAnimator.ResetTrigger("Triggered");
        gemAreaAnimator.SetTrigger("Triggered");

    } // TriggerCoinReachedAnimation()

    public void TriggerLevelEndCanvas(int _collectedGemAmount, int _multiplier, int _collectedSumGemAmount)
    {
        levelEndCanvas.SetActive(true);
        collectedGemAmountText.text = _collectedGemAmount.ToString();
        collectedGemSumAmountText.text = _collectedSumGemAmount.ToString();
        gemMultiplierText.text = "x" + _multiplier.ToString();

    } // TriggerLevelEndCanvas()

    public void TriggerCloseTutorialBar()
    {
        Destroy(tutorialSliderBarObject);

    } // TriggerCloseTutorialBar()


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




} // class
