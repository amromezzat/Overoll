using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (menuName ="Config/TileConfig")]
public class TileConfig : ScriptableObject
{
    public float tileSpeed;
    public float tileSize;
    public float laneWidth;
    [Header("Don't Change!")]
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
