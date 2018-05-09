using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Config/Tile Config")]
public class TileConfig : ScriptableObject
{
    public float tileSpeed;
    public float tileSize;
    public float laneWidth;
    [Header("Don't Change!")]
    public float spawnTime;//changed by game manager

    private void OnEnable()
    {
        CalculateSpawnTime();
    }

    public void CalculateSpawnTime()
    {
        spawnTime = tileSize / tileSpeed;
    }
}
