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
    public Animator settingsPanelAnimator;
    public GameObject vibrationOptionOpenedImage;
    public GameObject vibrationOptionClosedImage;
    public GameObject sfxOptionOpenedImage;
    public GameObject sfxOptionClosedImage;
    public Animator gearIconAnimator;
    public Animator sfxIconAnimator;
    public Animator vibrationIconAnimator;
    bool isGearOpened = false;
    bool isSFXOn = true;
    bool isVibrationOn = true;

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

        int tmpVib = -1;

        if (PlayerPrefs.HasKey("VibrationOff"))
        {
            tmpVib = PlayerPrefs.GetInt("VibrationOff");

            if (tmpVib == 0)
            {
                isVibrationOn = true;
            }
            else
            {
                isVibrationOn = false;
            }
        }

        int tmpSFX = -1;

        if (PlayerPrefs.HasKey("SFXOff"))
        {
            tmpSFX = PlayerPrefs.GetInt("SFXOff");

            if (tmpSFX == 0)
            {
                isSFXOn = true;
            }
            else
            {
                isSFXOn = false;
            }
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

    public void GearButtonPressed()
    {
        if (!isGearOpened)
        {
            GearIconOpeningTrigger();
        }
        else
        {
            GearIconClosingTrigger();
        }

    } // GearButtonPressed()

    public void GearIconOpeningTrigger()
    {
        gearIconAnimator.ResetTrigger("Close");
        gearIconAnimator.ResetTrigger("Open");
        gearIconAnimator.SetTrigger("Open");

        settingsPanelAnimator.ResetTrigger("Close");
        settingsPanelAnimator.ResetTrigger("Open");
        settingsPanelAnimator.SetTrigger("Open");

        sfxIconAnimator.ResetTrigger("Close");
        sfxIconAnimator.ResetTrigger("Open");
        sfxIconAnimator.SetTrigger("Open");

        vibrationIconAnimator.ResetTrigger("Close");
        vibrationIconAnimator.ResetTrigger("Open");
        vibrationIconAnimator.SetTrigger("Open");

        isGearOpened = true;
        sfxIconAnimator.gameObject.SetActive(true);
        vibrationIconAnimator.gameObject.SetActive(true);

        if (isSFXOn)
        {
            sfxOptionOpenedImage.SetActive(true);
            sfxOptionClosedImage.SetActive(false);
        }
        else
        {
            sfxOptionOpenedImage.SetActive(false);
            sfxOptionClosedImage.SetActive(true);
        }

        if (isVibrationOn)
        {
            vibrationOptionOpenedImage.SetActive(true);
            vibrationOptionClosedImage.SetActive(false);
        }
        else
        {
            vibrationOptionOpenedImage.SetActive(false);
            vibrationOptionClosedImage.SetActive(true);
        }

    } // GearIconOpeningTrigger()

    public void GearIconClosingTrigger()
    {
        gearIconAnimator.ResetTrigger("Open");
        gearIconAnimator.ResetTrigger("Close");
        gearIconAnimator.SetTrigger("Close");

        settingsPanelAnimator.ResetTrigger("Open");
        settingsPanelAnimator.ResetTrigger("Close");
        settingsPanelAnimator.SetTrigger("Close");

        sfxIconAnimator.ResetTrigger("Open");
        sfxIconAnimator.ResetTrigger("Close");
        sfxIconAnimator.SetTrigger("Close");

        vibrationIconAnimator.ResetTrigger("Open");
        vibrationIconAnimator.ResetTrigger("Close");
        vibrationIconAnimator.SetTrigger("Close");

        isGearOpened = false;

    } // GearIconClosingTrigger()

    public void SFXOptionButtonPressed()
    {
        if (isSFXOn)
        {
            isSFXOn = false;
            sfxOptionOpenedImage.SetActive(false);
            sfxOptionClosedImage.SetActive(true);
            //PlayerPrefs.SetInt("SFXOff", 1);
            AudioManager._instance.MuteSFX(true);
        }
        else
        {
            isSFXOn = true;
            sfxOptionOpenedImage.SetActive(true);
            sfxOptionClosedImage.SetActive(false);
            //PlayerPrefs.SetInt("SFXOff", 0);
            AudioManager._instance.UnMuteSFX(true);
        }

    } // SFXOptionButtonPressed()

    public void VibrationOptionButtonPressed()
    {
        if (isVibrationOn)
        {
            isVibrationOn = false;
            vibrationOptionOpenedImage.SetActive(false);
            vibrationOptionClosedImage.SetActive(true);
            PlayerPrefs.SetInt("VibrationOff", 1);
            GameManager._instance.isVibrationOn = false;
        }
        else
        {
            isVibrationOn = true;
            vibrationOptionOpenedImage.SetActive(true);
            vibrationOptionClosedImage.SetActive(false);
            PlayerPrefs.SetInt("VibrationOff", 0);
            GameManager._instance.isVibrationOn = true;
        }

    } // VibrationOptionButtonPressed()


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
