using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    float firstDistance, currentDistance;
    public Slider filledBar;
    public Transform finishPoint;
    public Transform playerPoint;

    private void Start()
    {
        finishPoint = GameObject.FindGameObjectWithTag("FinishPointForBar").transform;
        firstDistance = Mathf.Abs(finishPoint.position.z) - Mathf.Abs(playerPoint.position.z);

    } // Start()

    void Update()
    {
        currentDistance = Mathf.Abs(finishPoint.position.z) - Mathf.Abs(playerPoint.position.z);
        filledBar.value = (firstDistance - currentDistance) / firstDistance;

    } // Update()

} // class
