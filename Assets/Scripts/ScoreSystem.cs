using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
     int timeScore;
     int coinScore;
     int coinvalue= 5;
     int secValue = 1;
     int score;
    public int oldCoinCount;
    public Text scoreText;
    public Text coinnum;
    public GameData gstate;
    

    // Use this for initialization
    void Start () {
        timeScore = 0;
        coinScore = 0;
        
        StartCoroutine(scorepersec());
    }

    // Update is called once per frame
    void Update () {
        
        coinScore = coinvalue * (gstate.CoinCount-oldCoinCount) *gstate.workersNum ;
        

        // calc score
        score =  timeScore;
        //Display score
        scoreText.text = score.ToString();
        coinnum.text = gstate.CoinCount.ToString();
        oldCoinCount = gstate.CoinCount;
       
    }

    IEnumerator scorepersec()
    {
        yield return new WaitForSeconds(0.125f);
        timeScore += secValue;
        StartCoroutine(scorepersec());

    }
}
