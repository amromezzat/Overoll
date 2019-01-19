/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameData", menuName = "Config/GameData")]
public class GameData : ScriptableObject
{
    public bool tutorialActive = true;
    [SerializeField]
    private TutorialState tutorialState;

    //public int difficulty;
    // public float defaultSpeed;
    //private float speed;
    // [HideInInspector]
    //public float oldSpeed;

    //public int coinCount;

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

    // Handeled in a script called SpeedManager
    //public float Speed
    //{
    //    get
    //    {
    //        return speed;
    //    }
    //    set
    //    {
    //        oldSpeed = speed;
    //        speed = value;
    //    }
    //}

    public TutorialState TutorialState
    {
        get
        {
            return tutorialState;
        }

        set
        {
            tutorialState = value;

            if (tutorialActive && value != TutorialState.Null)
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
        //speed = defaultSpeed;
#if !UNITY_EDITOR
        tutorialActive = PlayerPrefs.GetFloat("PlayedTutorial") > 0 ? false : true;
#endif
        //>>>>>>>>>>>>> Handeled now in GameManager Script
        //difficulty = PlayerPrefs.GetInt("PlayedTutorial");
    }
}