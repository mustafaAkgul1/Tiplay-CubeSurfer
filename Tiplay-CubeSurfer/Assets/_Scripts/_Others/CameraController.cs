using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController _instance;

    [Header("General Variables")]
    public bool useFOVChange = true;
    public bool useCameraRotateWhenFinish = true;
    public float cubeCountFOVMultiplier;
    public float fovSmoothDampValue;
    public CameraStates cameraState;
    Vector3 offsetToPlayer;
    int currentCollectedCubeCount = 0;
    float targetFOVamount = 60f;
    float defFOVAmount = 60f;

    [Header("References")]
    public Transform playerTransform;
    public PlayerCubeDetectorController playerCubeScript;
    Camera cam;

    public enum CameraStates
    {
        OnFollow,
        OnSuccessFinish
    }

    private void Awake()
    {
        _instance = this;

    } // Awake()

    void Start()
    {
        cam = GetComponent<Camera>();
        offsetToPlayer = transform.position - playerTransform.position;
        defFOVAmount = cam.fieldOfView;

        targetFOVamount = defFOVAmount;

        //GetCollectedCubeCount();

    } // Start()

    void Update()
    {
        switch (cameraState)
        {
            case CameraStates.OnFollow:

                if (useFOVChange)
                {
                    CalculateFOVAmount();
                }

                //HandlePlayerFollow();

                break;

            case CameraStates.OnSuccessFinish:

                break;

            default:
                break;
        }

    } // Update()

    private void FixedUpdate()
    {
        if (cameraState == CameraStates.OnFollow)
        {
            HandlePlayerFollow();
        }

    } // FixedUpdate()

    void HandlePlayerFollow()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(transform.position.x, playerTransform.position.y + offsetToPlayer.y, playerTransform.position.z + offsetToPlayer.z);
        }

    } // HandlePlayerFollow()

    public void GetCollectedCubeCount() // calling from gamemanager( every cube interactions )
    {
        currentCollectedCubeCount = playerCubeScript.cubeStackParentTransform.childCount;
        targetFOVamount = defFOVAmount + (currentCollectedCubeCount * (cubeCountFOVMultiplier));

    } // GetCollectedCubeCount()

    void CalculateFOVAmount()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOVamount, fovSmoothDampValue);

    } // CalculateFOVAmount()

    public void TriggerLevelSuccessFinished(Transform _parent)
    {
        cameraState = CameraStates.OnSuccessFinish;

        if (useCameraRotateWhenFinish)
        {
            transform.parent = _parent;
        }

    } // TriggerLevelSuccessFinished()

} // class
