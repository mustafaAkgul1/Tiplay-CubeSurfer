﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeDetectorController : MonoBehaviour
{
    public Transform cubeDockTransform;
    public Transform cubeStackParentTransform;
    public float cubeDockYoffset;

    private void Start()
    {
        SetParentedCubesToAttached();

    } // Start()

    void SetParentedCubesToAttached()
    {
        for (int i = 0; i < cubeStackParentTransform.childCount; i++)
        {
            cubeStackParentTransform.GetChild(i).GetComponent<NormalCubeController>().TriggerCollected();
        }

    } // SetParentedCubesToAttached()

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NormalCubeBack"))
        {
            if (GameManager._instance.isGameStarted)
            {
                if (other.transform.parent.GetComponent<NormalCubeController>().cubeState == NormalCubeController.CubeStates.OnHold)
                {
                    other.transform.parent.position = cubeDockTransform.position;
                    other.transform.parent.parent = cubeStackParentTransform;
                    other.transform.parent.GetComponent<NormalCubeController>().TriggerCollected();
                    cubeDockTransform.position += Vector3.up * cubeDockYoffset;

                    //GameManager._instance.TriggerCamFOV();
                    GameManager._instance.TriggerCubeCollect();
                    UIManager._instance.CreatePopUpText(other.transform.parent);
                    VFXManager._instance.SpawnCubeCollectedVFX(other.transform.parent);
                }
            }
        }

    } // OnTriggerEnter()

    public void CheckHasCubeLeft()
    {
        if (cubeStackParentTransform.childCount <= 0)
        {
            // playerı taşıyan küp kalmadı, ölüm trigger
            PlayerMovementController._instance.TriggerMovementStoppedWithDeath();
            GameManager._instance.TriggerLevelFailed();
        } 

    } // CheckHasCubeLeft()

    public bool CheckHasOneCubeLeftForSuccessFinish()
    {
        bool tmp = false;

        if (cubeStackParentTransform.childCount == 1)
        {
            tmp = true;
        }

        return tmp;

    } // CheckHasOneCubeLeftForSuccessFinish()

} //class
