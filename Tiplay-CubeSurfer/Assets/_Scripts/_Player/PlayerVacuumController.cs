using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVacuumController : MonoBehaviour
{
    public GameObject vacuumCollObject;
    public float magnetPowerDuration;

    public void TriggerMagnetActive()
    {
        StopCoroutine(DelayedDeactivationOfMagnet());
        vacuumCollObject.SetActive(true);
        StartCoroutine(DelayedDeactivationOfMagnet());

    } // TriggerMagnetActive()

    IEnumerator DelayedDeactivationOfMagnet()
    {
        yield return new WaitForSeconds(magnetPowerDuration);

        vacuumCollObject.SetActive(false);

    } // DelayedDeactivationOfMagnet()

} // class
