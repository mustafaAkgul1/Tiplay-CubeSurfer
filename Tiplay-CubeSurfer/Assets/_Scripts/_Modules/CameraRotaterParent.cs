using UnityEngine;

public class CameraRotaterParent : MonoBehaviour
{
    [Header("General Variables")]
    public bool canRotate = false;
    public float rotateSpeed;
    
    void Update()
    {
        if (canRotate)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

    } // Update()

} // class
