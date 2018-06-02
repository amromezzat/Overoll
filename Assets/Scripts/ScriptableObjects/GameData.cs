using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameState", menuName = "Config/GameState")]
public class GameData : ScriptableObject
{
    [HideInInspector]
    public int difficulty;
    public int workersNum;
    [HideInInspector]
    public int hrNum;
    [HideInInspector]
    public int bossNum;
    [HideInInspector]
    public PoolableType leaderType;

    public GameState gameState;

    [HideInInspector]
    public UnityEvent OnStart;
    [HideInInspector]
    public UnityEvent OnResume;
    [HideInInspector]
    public UnityEvent onPause;
    [HideInInspector]
    public UnityEvent onEnd;

    [HideInInspector]
    public int CoinCount;

    public int safeZone = -5;

    private void OnEnable()
    {
        gameState = GameState.MainMenu;
    }

}

