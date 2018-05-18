using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Stats", menuName = "Config/GameStat")]
public class GameStats : ScriptableObject
{
    public int difficulty;
    [HideInInspector]
    public int workersNum;
    [HideInInspector]
    public int hrNum;
    [HideInInspector]
    public int bossNum;
    [HideInInspector]
    public TileType leadrType;
}

