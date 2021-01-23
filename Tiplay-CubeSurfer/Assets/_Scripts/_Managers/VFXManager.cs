using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager _instance;

    [Header("General Variables")]
    public float confettiLoopRate;

    [Header("VFX Prefabs")]
    public GameObject levelSuccessConfettiVFX;
    public GameObject testVFX;

    private void Awake()
    {
        _instance = this;
    }

    public void SpawnTestVFX(Transform _posTransform)
    {
        Vector3 pos = new Vector3(_posTransform.position.x, _posTransform.position.y + 0f, _posTransform.position.z);

        Instantiate(testVFX, pos, Quaternion.Euler(-90f, 0f, 0f));

    } // SpawnTestVFX()


    public void StartConfettiLoop(Transform _posTransform)
    {
        StartCoroutine(ConfettiLoop(_posTransform.position));

    } // SpawnLevelSuccessConfettiVFX()

    IEnumerator ConfettiLoop(Vector3 _pos)
    {
        while (true)
        {
            yield return new WaitForSeconds(confettiLoopRate);

            Vector3 rndPos = new Vector3(Random.Range(_pos.x - 2f, _pos.x + 2f), _pos.y + 5f, Random.Range(_pos.z - 2f, _pos.z + 2f));
            Instantiate(levelSuccessConfettiVFX, rndPos, Quaternion.Euler(-90f, 0f, 0f));
        }

    } // ConfettiLoop()

} // class
