using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCubeController : MonoBehaviour
{
    [Header("General Variables")]
    bool canTrail = false;
    public CubeStates cubeState;

    [Header("References")]
    public GameObject backCheckCollObject;
    public TrailRenderer trailRenderer;

    public enum CubeStates
    {
        OnHold,
        InParent
    }

    public void TriggerCollected()
    {
        cubeState = CubeStates.InParent;
        Destroy(backCheckCollObject);

    } // TriggerCollected()

    private void Update()
    {
        trailRenderer.enabled = canTrail;

    } // Update()

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (cubeState == CubeStates.InParent)
            {
                canTrail = true;
            }
        }

    } // OnCollisionStay()

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canTrail = false;
        }

    } // OnCollisionExit()

} // class
