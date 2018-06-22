using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public GameData gd;
    public WorkerConfig wc;
    float magnetTime=0.0f;
    private CoinMagnetTrial2 coinMagnet;

    private void Awake()
    {
        coinMagnet = GetComponent<CoinMagnetTrial2>();

    }
    private void Update()
    {
        if (gd.magnetAct)
        {
            magnetTime -= Time.deltaTime;
            if (magnetTime < 0)
            {
                gd.magnetAct = false;
                wc.endMagnet.Invoke();
            }
        }
        
    }

    public void RegisterListeners()
    {
        wc.gotMagnet.AddListener(StartTimer);
        wc.endMagnet.AddListener(EndMagnetAct);

    }
    void EndMagnetAct()
    {
        coinMagnet.enabled = false;
    }

    void StartTimer()
    {
        gd.magnetAct = true;
        magnetTime = 5f;
    }
}
