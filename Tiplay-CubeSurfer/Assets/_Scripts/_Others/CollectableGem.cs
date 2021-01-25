using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGem : MonoBehaviour
{
    public int gemAmount = 10;
    public GameObject gemImage;
    public Ease movingGemEaseType;
    Transform uiCanvasObject;

    void Start()
    {
        uiCanvasObject = UIManager._instance.uiCanvasObject;

    } // Start()

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerParent"))
        {
            VFXManager._instance.SpawnGemCollectedVFX(other.transform);

            Vector3 createPos = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0f, 1f, 0f));

            GameObject tmp = Instantiate(gemImage, createPos, Quaternion.identity, uiCanvasObject);

            tmp.transform.DOScale(tmp.transform.localScale * 1.2f, 0.15f).SetEase(Ease.InSine).OnComplete(() => 
            tmp.transform.DOScale(tmp.transform.localScale / 4f, 0.35f).SetEase(Ease.OutSine));

            tmp.transform.DOMove(UIManager._instance.gemImage.transform.position, .5f).SetEase(movingGemEaseType).OnComplete(() =>
            {
                UIManager._instance.IncreaseGemAmountText(gemAmount);
                Destroy(tmp);
            });

            GameManager._instance.IncreaseCollectedGemAmount();

            Destroy(gameObject);
        }

    } // OnTriggerEnter()

} // class
