using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCubeController : MonoBehaviour
{
    [Header("General Variables")]
    bool canTrail = false;
    public CubeStates cubeState;
    Vector3 defScale;

    [Header("References")]
    public GameObject backCheckCollObject;
    public TrailRenderer trailRenderer;

    public enum CubeStates
    {
        OnHold,
        InParent
    }

    private void Start()
    {
        defScale = transform.localScale;
    }

    public void TriggerCollected()
    {
        if (cubeState == CubeStates.OnHold)
        {
            AudioManager._instance.PlayCubeCollectSFX();

            cubeState = CubeStates.InParent;
            Destroy(backCheckCollObject);

            if (DOTween.IsTweening("CollectibleNormalCubeScaleTween"))
            {
                DOTween.Kill("CollectibleNormalCubeScaleTween");
            }

            transform.localScale = defScale;
            Destroy(GetComponent<CollectableVacuumModule>());
        }

    } // TriggerCollected()

    private void Update()
    {
        trailRenderer.enabled = canTrail;

    } // Update()

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("FinishFloorForTrail"))
        {
            if (cubeState == CubeStates.InParent)
            {
                canTrail = true;
            }
        }

    } // OnCollisionStay()

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("FinishFloorForTrail"))
        {
            canTrail = false;
        }

    } // OnCollisionExit()

} // class
