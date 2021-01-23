using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    float FirstDistance, currentDistance;
    [SerializeField] Image filledBar;
    [SerializeField] Transform Finish,Player;

    private void Start()
    {
        Finish = GameObject.FindGameObjectWithTag("FinishObjectForBar").transform;
        FirstDistance = Mathf.Abs(Finish.position.z) - Mathf.Abs(Player.position.z);

    } // Start()

    void Update()
    {
        currentDistance = Mathf.Abs(Finish.position.z) - Mathf.Abs(Player.position.z);
        filledBar.fillAmount = (FirstDistance - currentDistance) / FirstDistance;

    } // Update()

} // class
