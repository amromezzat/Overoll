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
                for (int i = 0; i < wc.workers.Count; i++)
                {
                    wc.workers[i].helmetMaterial.SetFloat("_ColAmount", 0);
                }
            }
        }

        if (gd.shieldInAct)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer < 0)
            {
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
        if (gd.magnetInAct)
        {
            gd.magnetInAct = false;
        }
        gd.shieldInAct = true;
        shieldTimer = gd.ShieldTime;
        for (int i = 0; i < wc.workers.Count; i++)
        {
            wc.workers[i].health = 1000;
            wc.workers[i].helmetMaterial.SetFloat("_ExtAmount", 0.0001f);
        }
    }

    void EndShield()
    {
        gd.shieldInAct = false;
        wc.workers.ResetWorkersHealth();
    }

    void StartMagnetTimer()
    {
        if (gd.shieldInAct)
        {
            EndShield();
        }
        for (int i = 0; i < wc.workers.Count; i++)
        {
            wc.workers[i].helmetMaterial.SetFloat("_ColAmount", -0.001f);
        }
        gd.magnetInAct = true;
        magnetTimer = gd.MagnetTime;
    }
}
