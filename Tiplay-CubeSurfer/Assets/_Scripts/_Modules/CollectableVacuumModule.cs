using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableVacuumModule : MonoBehaviour
{
    public float scalingTime = 1f;
    public float moveSpeed = 10f;
    bool check = false;
    Transform playerTransform;
    public CollectableTypes collectableType;

    public enum CollectableTypes
    {
        Gem,
        NormalCube
    }

    void Start()
    {
        playerTransform = PlayerMovementController._instance.transform;
    }

    void Update()
    {
        if (check)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position + new Vector3(0f, 0.5f, 0f), moveSpeed * Time.deltaTime);
        }

    } // Update()

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVacuum"))
        {
            check = true;

            if (gameObject != null)
            {
                switch (collectableType)
                {
                    case CollectableTypes.Gem:
                        transform.DOScale(new Vector3(0.15f, 0.15f, 0.15f), scalingTime).SetId("CollectibleGemScaleTween");
                        break;
                    case CollectableTypes.NormalCube:
                        transform.DOScale(new Vector3(0.15f, 0.15f, 0.15f), scalingTime).SetId("CollectibleNormalCubeScaleTween");
                        break;
                    default:
                        break;
                }
            }
        }

    } // OnTriggerEnter()

} // class
