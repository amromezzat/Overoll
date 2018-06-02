using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Config/TileConfig")]
public class TileConfig : ScriptableObject
{
    public float tileSpeed = 5;
    public float tileSize = 1;
    public float laneWidth = 1;
    public float disableSafeDistance = 10;

    [HideInInspector]
    public float spawnTime;//changed by game manager
    [HideInInspector]
    public UnityEvent produceNextSegment;

    private void OnEnable()
    {
        CalculateSpawnTime();
    }

    public void CalculateSpawnTime()
    {
        spawnTime = tileSize / tileSpeed;
    }
}
