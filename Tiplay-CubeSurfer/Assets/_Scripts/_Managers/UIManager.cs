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
    public TextMeshProUGUI gemAmountText;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("GemAmount"))
        {
            gemAmountText.text = PlayerPrefs.GetInt("GemAmount").ToString();
        }

    } // Start()

    public void CreatePopUpText(Transform _transform)
    {
        GameObject tmp = Instantiate(popUpTextPrefab, _transform.position + (Vector3.right * 1.5f), Quaternion.identity, _transform);
        Destroy(tmp, 0.65f);

    } // CreatePopUpText

    public void IncreaseGemAmountText(int _amount)
    {
        int tmp = int.Parse(gemAmountText.text);
        tmp += _amount;
        gemAmountText.text = tmp.ToString();

        PlayerPrefs.SetInt("GemAmount", tmp);

    } // IncreaseCoinText()

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
