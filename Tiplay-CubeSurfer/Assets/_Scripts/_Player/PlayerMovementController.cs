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

    [Header("References")]
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
            HandleEndlessRun();
            HandleMovementWithSlide();
        }

    } // Update()

    public void TriggerGameStarted()
    {
        canMove = true;

    } // TriggerGameStarted()

    void AttachReferences()
    {
        rbPlayer = GetComponent<Rigidbody>();
        mainCam = Camera.main;

    } // AttachReferences()

    void ResetInputValues()
    {
        currentMoveSpeed = defMoveSpeed;

        rbPlayer.velocity = Vector3.zero;
        firstTouchPosition = Vector2.zero;
        finalTouchX = 0f;
        curTouchPosition = Vector2.zero;

    } // ResetInputValues()

    void HandleEndlessRun()
    {
        rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, rbPlayer.velocity.y, currentMoveSpeed * Time.deltaTime);

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

} // class
