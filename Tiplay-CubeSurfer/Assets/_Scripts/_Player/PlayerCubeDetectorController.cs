using System.Collections;
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
                other.transform.parent.position = cubeDockTransform.position;
                other.transform.parent.parent = cubeStackParentTransform;
                other.transform.parent.GetComponent<NormalCubeController>().TriggerCollected();
                cubeDockTransform.position += Vector3.up * cubeDockYoffset;

                GameManager._instance.IncreaseCamFOV();
            }
        }

    } // OnTriggerEnter()

} //class
