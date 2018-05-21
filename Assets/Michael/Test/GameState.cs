using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameState", menuName = "Config/GameState")]
public class GameState : ScriptableObject
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

    [HideInInspector]
    public bool isPaused;

    public UnityEvent OnResume;
    public UnityEvent onPause;

    private void OnEnable()
    {
        isPaused = false;
    }

}

