using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameData gd;
    public WorkerConfig wc;
    float magnetTimer;
    float shieldTimer;

    private void Awake()
    {
        RegisterListeners();
    }
    private void Update()
    {
        if (gd.magnetInAct)
        {
            magnetTimer -= Time.deltaTime;
            if (magnetTimer < 0)
            {
                gd.magnetInAct = false;
            }
        }

        if (gd.shieldInAct)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer < 0)
            {
                gd.shieldInAct = false;
                EndShield();
            }
        }
    }

    public void RegisterListeners()
    {
        gd.gotMagnet.AddListener(StartMagnetTimer);
        gd.gotShield.AddListener(ActWithShield);
    }

    void ActWithShield()
    {
        shieldTimer = gd.shieldTime;
        gd.shieldInAct = true;
        for (int i = 0; i < wc.workers.Count; i++)
        {
            wc.workers[i].health = 1000;
        }
    }

    void EndShield()
    {
        gd.shieldInAct = false;
        wc.workers.ResetWorkersHealth();
    }

    void StartMagnetTimer()
    {
        magnetTimer = gd.magnetTime;
        gd.magnetInAct = true;
    }
}
