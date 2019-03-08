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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour, IHalt
{
    public static ScoreManager Instance;

    public IntField coinsCount;
    public IntField score;

    int timeScore;
    int coinvalue = 5;
    int secValue = 1;
    // public int oldCoinCount;
    public Text scoreText;
    public Text coinNum;
    public WorkerConfig wConfig;
    public IEnumerator scoreCoroutine;

    [HideInInspector]
    public int workerPrice = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        coinsCount.Value = PlayerPrefs.GetInt("CoinsCountGet");
        scoreCoroutine = ScorePerSec();
    }

    // Use this for initialization
    void OnEnable()
    {
        timeScore = 0;
        //gData.coinCount = 0;
        //oldCoinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RegisterListeners();
        if (TutorialManager.Instance.tutorialActive)
            return;

        score.Value = timeScore + coinsCount.Value;


        //for leaderboard
        //LBUIscript.Instance.UpdatePointsTxt();

        //Display score
        scoreText.text = score.Value.ToString();
        coinNum.text = coinsCount.Value.ToString();
    }

    IEnumerator ScorePerSec()
    {
        while (true)
        {
            timeScore += secValue;
            yield return new WaitForSeconds(0.125f);
        }
    }

    public void RegisterListeners()
    {
        GameManager.Instance.OnStart.AddListener(Begin);
        GameManager.Instance.onPause.AddListener(Halt);
        GameManager.Instance.OnResume.AddListener(Resume);
        GameManager.Instance.onEnd.AddListener(End);
    }

    public void Begin()
    {
        timeScore = 0;
        //gData.coinCount = 0;
        //oldCoinCount = 0;
        StartCoroutine(scoreCoroutine);
    }

    public void Halt()
    {
        StopCoroutine(scoreCoroutine);
    }

    public void Resume()
    {
        StartCoroutine(scoreCoroutine);
    }

    public void End()
    {
        Halt();
    }

    public void DeductWorkerPrice()
    {
        coinsCount.Value -= workerPrice;
    }
}
