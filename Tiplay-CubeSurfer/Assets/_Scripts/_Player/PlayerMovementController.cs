using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController _instance;

    [Header("General Variables")]
    public bool canMove = false;
    public float defMoveSpeed;
    public float decreasedMoveSpeed;
    float currentMoveSpeed;
    public float sensitivityMultiplier;
    public float deltaThreshold;
    Vector2 firstTouchPosition;
    float finalTouchX, finalTouchZ;
    Vector2 curTouchPosition;
    public float minXPos;
    public float maxXPos;
    int currentMultiplierZone = 1;

    [Header("References")]
    public PlayerCubeDetectorController playerCubeDetectorScript;
    public PlayerAnimatorController playerAnimScript;
    public CameraRotaterParent cameraRotaterParent;
    public PlayerVacuumController playerVacuumScript;
    Rigidbody rbPlayer;
    Camera mainCam;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        AttachReferences();
        ResetInputValues();

    } // Start()

    void Update()
    {
        if (canMove)
        {
            //HandleEndlessRun();
            HandleMovementWithSlide();
        }

    } // Update()

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleEndlessRun();
        }

    } // FixedUpdate()

    public void TriggerGameStarted()
    {
        canMove = true;

    } // TriggerGameStarted()

    void AttachReferences()
    {
        rbPlayer = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        currentMoveSpeed = defMoveSpeed;

    } // AttachReferences()

    void ResetInputValues()
    {
        rbPlayer.velocity = new Vector3(0f, rbPlayer.velocity.y, rbPlayer.velocity.z);
        firstTouchPosition = Vector2.zero;
        finalTouchX = 0f;
        curTouchPosition = Vector2.zero;

    } // ResetInputValues()

    void HandleEndlessRun()
    {
        rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, rbPlayer.velocity.y, currentMoveSpeed * Time.fixedDeltaTime);

    } // HandleEndlessRun()

    void HandleMovementWithSlide()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            curTouchPosition = Input.mousePosition;
            Vector2 touchDelta = (curTouchPosition - firstTouchPosition);

            if (firstTouchPosition == curTouchPosition)
            {
                rbPlayer.velocity = new Vector3(0f, rbPlayer.velocity.y, rbPlayer.velocity.z);
            }

            //Debug.Log("firstTPos : " + firstTouchPosition + " - curTPos : " + curTouchPosition + " - touchDelta : " + touchDelta);
            finalTouchX = transform.position.x;

            if (Mathf.Abs(touchDelta.x) >= deltaThreshold)
            {
                finalTouchX = (transform.position.x + (touchDelta.x * sensitivityMultiplier));
            }

            rbPlayer.position = new Vector3(finalTouchX, transform.position.y, transform.position.z);
            rbPlayer.position = new Vector3(Mathf.Clamp(rbPlayer.position.x, minXPos, maxXPos), rbPlayer.position.y, rbPlayer.position.z);

            firstTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetInputValues();
        }

    } // HandleMovementWithSlide()

    public void TriggerLevelFinished()
    {
        canMove = false;

    } // TriggerLevelFinished()

    public void TriggerCubeLostForSpeedDecrease()
    {
        CheckHasCubesForDeath();

        if (DOTween.IsTweening("PlayerSpeedDecreaseTween"))
        {
            DOTween.Kill("PlayerSpeedDecreaseTween", true);
        }

        if (DOTween.IsTweening("PlayerSpeedIncreaseTween"))
        {
            DOTween.Kill("PlayerSpeedIncreaseTween", true);
        }

        DOTween.To(() => currentMoveSpeed, x => currentMoveSpeed = x, decreasedMoveSpeed, 0.1f).SetId("PlayerSpeedDecreaseTween").OnComplete(() => {

            DOTween.To(() => currentMoveSpeed, x => currentMoveSpeed = x, defMoveSpeed, 0.1f).SetId("PlayerSpeedIncreaseTween");
        });

    } // TriggerCubeLostForSpeedDecrease()

    void CheckHasCubesForDeath()
    {
        playerCubeDetectorScript.CheckHasCubeLeft();

    } // CheckHasCubesForDeath()

    public void TriggerMovementStopped()
    {
        canMove = false;
        rbPlayer.velocity = Vector3.zero;

    } // TriggerMovementStopped()

    public void TriggerMovementStoppedWithDeath()
    {
        TriggerMovementStopped();
        playerAnimScript.TriggerRagDollDeath();
        CameraController._instance.cameraState = CameraController.CameraStates.OnSuccessFinish;

    } // TriggerMovementStoppedWithDeath()

    public void TriggerFinishFloorUpperPart()
    {
        if (playerCubeDetectorScript.CheckHasOneCubeLeftForSuccessFinish())
        {
            TriggerMovementStopped();
            GameManager._instance.TriggerLevelSuccessed(currentMultiplierZone);
            playerAnimScript.TriggerDance();
            cameraRotaterParent.canRotate = true;
            CameraController._instance.TriggerLevelSuccessFinished(cameraRotaterParent.transform);
            VFXManager._instance.StartConfettiLoop(transform);
        }
        else
        {
            if (DOTween.IsTweening("PlayerFinishFloorUpperTween"))
            {
                DOTween.Kill("PlayerFinishFloorUpperTween");
            }

            rbPlayer.DOMoveY(rbPlayer.position.y + 1.15f, 0.1f).SetId("PlayerFinishFloorUpperTween");
        }

    } // TriggerFinishFloorUpperPart()

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killzone"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("FinishFloor"))
        {
            currentMultiplierZone = other.gameObject.GetComponent<FinishFloorController>().currentMultiplierZone;
        }

        if (other.CompareTag("LatestStopperColl"))
        {
            currentMultiplierZone = 10;

            TriggerMovementStopped();
            GameManager._instance.TriggerLevelSuccessed(currentMultiplierZone);
            playerAnimScript.TriggerDance();
            cameraRotaterParent.canRotate = true;
            CameraController._instance.TriggerLevelSuccessFinished(cameraRotaterParent.transform);
            VFXManager._instance.StartConfettiLoop(transform);
        }

        if (other.CompareTag("MagnetCollectable"))
        {
            Destroy(other.gameObject);
            playerVacuumScript.TriggerMagnetActive();
        }

    } // OnTriggerEnter()

} // class
