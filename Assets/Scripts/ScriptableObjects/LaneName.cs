using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaneName", menuName = "Config/Lane/LaneName")]
public class LaneName : ScriptableObject {
    private int laneNum;
    public float laneCenter;

    public int LaneNum
    {
        get
        {
            return laneNum;
        }
    }
}
