using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - playerTransform.position;

    } // Start()

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z + offset.z);

    } // Update()


} // class
