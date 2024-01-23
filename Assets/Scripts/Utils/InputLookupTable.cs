using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputLookupTable
{
    public static Dictionary<Vector3, LimbNames> InputToLimbNames = new Dictionary<Vector3, LimbNames>()
    {
        {new Vector3(0, 1, 0), LimbNames.RightRearLeg },
        {new Vector3(0, -1, 0), LimbNames.LeftRearLeg },
        {new Vector3(-1, 0, 0), LimbNames.RightFrontLeg },
        {new Vector3(1, 0, 0), LimbNames.LeftFrontLeg },
        {new Vector3(0, 0, 1), LimbNames.Neck },
        {new Vector3(0, 0, -1), LimbNames.Tongue },
    };
}
