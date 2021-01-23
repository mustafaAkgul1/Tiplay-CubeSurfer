using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatingSpeed = 10f;
    
    void Start()
    {
        //transform.localPosition += new Vector3(-1f, Random.Range(0.1f, 0.5f), 0);
        transform.localPosition += new Vector3(0f, Random.Range(0.1f, 0.4f), 0);
    }

    void Update()
    {
        transform.localPosition += new Vector3(0, 0.1f, 0) * floatingSpeed * Time.deltaTime;
    }

} // sınıf
