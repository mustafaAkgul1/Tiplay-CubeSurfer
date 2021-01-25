using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFloorController : MonoBehaviour
{
    [HideInInspector] public int currentMultiplierZone = 0;

    public FinishFloorTypes finishFloorType;

    public enum FinishFloorTypes
    {
        x1,
        x2,
        x3,
        x4,
        x5,
        x6,
        x7,
        x8,
        x9,
        x10,
        FinishFlag
    }

    private void Start()
    {
        InitMultiplierValue();

    } // Start()

    void InitMultiplierValue()
    {
        switch (finishFloorType)
        {
            case FinishFloorTypes.x1:
                currentMultiplierZone = 1;
                break;
            case FinishFloorTypes.x2:
                currentMultiplierZone = 2;
                break;
            case FinishFloorTypes.x3:
                currentMultiplierZone = 3;
                break;
            case FinishFloorTypes.x4:
                currentMultiplierZone = 4;
                break;
            case FinishFloorTypes.x5:
                currentMultiplierZone = 5;
                break;
            case FinishFloorTypes.x6:
                currentMultiplierZone = 6;
                break;
            case FinishFloorTypes.x7:
                currentMultiplierZone = 7;
                break;
            case FinishFloorTypes.x8:
                currentMultiplierZone = 8;
                break;
            case FinishFloorTypes.x9:
                currentMultiplierZone = 9;
                break;
            case FinishFloorTypes.x10:
                currentMultiplierZone = 10;
                break;
            case FinishFloorTypes.FinishFlag:
                currentMultiplierZone = 10;
                break;
            default:
                break;
        }

    } // InitMultiplierValue()

} // class
