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
            //EventManager.onCubeLostEvent.Invoke();

            Destroy(transform.parent.gameObject, 2f);
        }

        if (other.CompareTag("FinishFloorUpTrigger"))
        {
            PlayerMovementController._instance.TriggerFinishFloorUpperPart();
            transform.parent.parent = null;
            GameManager._instance.TriggerCamFOV();
            //EventManager.onCubeLostEvent.Invoke();

            Destroy(transform.parent.gameObject, 2f);
        }

    } // OnTriggerEnter()


} // class
