using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator anim;

    private void OnEnable()
    {
        EventManager.onCubeLostEvent.AddListener(TriggerJumped);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NormalCube"))
        {
            TriggerLanded();
        }

    } // OnCollisionEnter()

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("NormalCube"))
        {
            TriggerJumped();
        }

    } // OnCollisionExit()

} // class
