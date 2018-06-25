using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public GameData gd;
    public WorkerConfig wc;
    float magnetTime=0.0f;
    float shieldTime = 0.0f;
    private CoinMagnet coinMagnet;
    List<int> oldHealth = new List<int>();

    private void Awake()
    {
        coinMagnet = GetComponent<CoinMagnet>();
        RegisterListeners();

    }
    private void Update()
    {
        if (gd.magnetInAct)
        {
            magnetTime -= Time.deltaTime;
            if (magnetTime < 0)
            {
                gd.magnetInAct = false;
            }
        }

        if (gd.shieldInAct)
        {
            shieldTime -= Time.deltaTime;
            if(shieldTime <0)
            {
                gd.shieldInAct = false;
                ActWithEndShield();

            }

        }
        
    }

    public void RegisterListeners()
    {
        gd.gotMagnet.AddListener(StartTimer);
        gd.gotShield.AddListener(ActWithShield);
   

    }
    void ActWithShield()
    {
        shieldTime = 5f;
        gd.shieldInAct = true;
        for (int i = 0; i < wc.workers.Count; i++)
        {
            oldHealth.Add(wc.workers[i].health);
            wc.workers[i].health = 1000;
        }

    }

    void ActWithEndShield()
    {
        gd.shieldInAct = false;
        for (int i = 0; i < wc.workers.Count; i++)
        {
            wc.workers[i].health = oldHealth[i];
        }
        oldHealth = new List<int>();
    }

    void StartTimer()
    {
        gd.magnetInAct = true;
        magnetTime = 5f;
    }
}
