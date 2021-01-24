using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCubeFrontCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleCube"))
        {
            transform.parent.parent = null;

            PlayerMovementController._instance.TriggerCubeLostForSpeedDecrease();
            GameManager._instance.TriggerCamFOV();
        }

    } // OnTriggerEnter()


} // class
