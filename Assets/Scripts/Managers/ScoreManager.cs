using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour, iHalt
{
    int timeScore;
    int coinScore;
    int coinvalue = 5;
    int secValue = 1;
    int score;
    public int oldCoinCount;
    public Text scoreText;
    public Text coinNum;
    public GameData gData;
    public IEnumerator scoreCoroutine;


    // Use this for initialization
    void OnEnable()
    {
        timeScore = 0;
        coinScore = 0;
        oldCoinCount = 0;
        scoreCoroutine = ScorePerSec();
        RegisterListeners();
    }

    // Update is called once per frame
    void Update()
    {

        coinScore = coinvalue * (gData.CoinCount - oldCoinCount) * gData.workersNum;
        // calc score
        score = timeScore;
        //Display score
        scoreText.text = score.ToString();
        coinNum.text = gData.CoinCount.ToString();
        oldCoinCount = gData.CoinCount;

    }

    IEnumerator ScorePerSec()
    {
        while (true)
        {
            timeScore += secValue;
            yield return new WaitForSeconds(0.125f);
        }
    }

    public void Halt()
    {
        StopCoroutine(scoreCoroutine);
    }

    public void Resume()
    {
        StartCoroutine(scoreCoroutine);
    }

    public void RegisterListeners()
    {
        gData.OnStart.AddListener(Halt);
        gData.onPause.AddListener(Halt);
        gData.OnResume.AddListener(Resume);
    }
}
