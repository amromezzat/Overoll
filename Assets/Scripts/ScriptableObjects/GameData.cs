using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public enum GameState
    {
        startState,
        gamePlayState,
        pauseState,
        gameEnded
    };

[CreateAssetMenu(fileName = "GameState", menuName = "Config/GameState")]
public class GameData : ScriptableObject
{
    public int difficulty;
    [HideInInspector]
    public int workersNum;
    [HideInInspector]
    public int hrNum;
    [HideInInspector]
    public int bossNum;
    [HideInInspector]
    public PoolableType leaderType;

    public GameState gameState;

    public UnityEvent OnStart;
    public UnityEvent OnResume;
    public UnityEvent onPause;
    public UnityEvent onEnd;

    public int CoinCount;

    public int safeZone = -5;

    private void OnEnable()
    {
        gameState = GameState.startState;
    }

}

