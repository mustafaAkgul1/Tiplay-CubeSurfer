using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [Header("References")]
    Animator anim;
    public Transform rigTransform;
    bool isDeathTriggered = false;

    private void OnEnable()
    {
        //EventManager.onCubeLostEvent.AddListener(TriggerJumped);

    } // OnEnable()

    void Start()
    {
        anim = GetComponent<Animator>();

    } // Start()

    private void OnDisable()
    {
        //EventManager.onCubeLostEvent.RemoveListener(TriggerJumped);

    } // OnDisable()

    public void TriggerJumped()
    {
        anim.SetBool("Falling", true);

    } // TriggerJumped()

    public void TriggerLanded()
    {
        anim.SetBool("Falling", false);

    } //  TriggerLanded()

    public void TriggerDance()
    {
        anim.ResetTrigger("Dance");
        anim.SetTrigger("Dance");

    } // TriggerDance()

    public void TriggerRagDollDeath()
    {
        if (!isDeathTriggered)
        {
            isDeathTriggered = true;

            anim.enabled = false;

            foreach (Rigidbody item in rigTransform.GetComponentsInChildren<Rigidbody>())
            {
                item.isKinematic = false;
                item.useGravity = true;
            }

            foreach (Collider item in rigTransform.GetComponentsInChildren<Collider>())
            {
                item.tag = "Untagged";
                item.isTrigger = false;
            }
        }

    } // TriggerRagDollDeath()

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NormalCube"))
        {
            TriggerLanded();
        }

    } // OnTriggerEnter()

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NormalCube"))
        {
            TriggerLanded();
        }

    } // OnTriggerStay()

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NormalCube"))
        {
            TriggerJumped();
        }

    } // OnTriggerExit()



} // class
