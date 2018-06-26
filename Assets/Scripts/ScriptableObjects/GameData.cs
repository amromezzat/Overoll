using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameData", menuName = "Config/GameData")]
public class GameData : ScriptableObject
{
    public bool tutorialActive = true;
    public TutorialState tutorialState;

    public int difficulty;
    public float defaultSpeed;
    private float speed;
    [HideInInspector]
    public float oldSpeed;

    public float slowingRatio = 0.1f;
    public float slowingRate = 0.5f;

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

    [HideInInspector]
    public UnityEvent gotMagnet;
    [HideInInspector]
    public UnityEvent gotShield;
    [HideInInspector]
    public UnityEvent gotMagnetNoMore;

    [HideInInspector]
    public UnityEvent onSpeedUp;
    [HideInInspector]
    public UnityEvent onSlowDown;

    public int workerPrice = 0;

    [HideInInspector]
    public bool magnetInAct = false;
    [HideInInspector]
    public bool shieldInAct = false;
    private float magnetTime = 5;
    private float shieldTime = 5;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            oldSpeed = speed;
            speed = value;
        }
    }

    public TutorialState TutorialState
    {
        get
        {
            return tutorialState;
        }

        set
        {
            tutorialState = value;
            if (value != TutorialState.Null)
            {
                onSlowDown.Invoke();
            }
        }
    }

    public float MagnetTime
    {
        get
        {
            return magnetTime;
        }

        private set
        {
            magnetTime = value;
        }
    }

    public float ShieldTime
    {
        get
        {
            return shieldTime;
        }

        private set
        {
            shieldTime = value;
        }
    }

    private void OnEnable()
    {
        gameState = GameState.MainMenu;
        tutorialState = TutorialState.Null;
        speed = defaultSpeed;
        difficulty = 0;
    }

}

