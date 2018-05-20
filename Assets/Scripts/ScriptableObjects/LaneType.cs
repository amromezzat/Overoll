using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaneType", menuName = "Config/Types/Lane")]
public class LaneType : ScriptableObject {
    [SerializeField]
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
