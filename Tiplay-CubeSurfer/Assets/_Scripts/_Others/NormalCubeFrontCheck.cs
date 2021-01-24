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

            GameManager._instance.DecreaseCamFOV();
        }

    } // OnTriggerEnter()


} // class
