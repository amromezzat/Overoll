using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Config/TileConfig")]
public class TileConfig : ScriptableObject
{
    public float tileSize = 1;
    public float laneWidth = 1;
    public float disableSafeDistance = 10;
    public float activeTilesNum = 30;
    public float keepInLaneForce = 1;
}
