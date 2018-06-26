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
        if (gd.shieldInAct)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer < 0)
            {
                EndShield();
            }
        }

        else if (gd.magnetInAct)
        {
            magnetTimer -= Time.deltaTime;
            if (magnetTimer < 0)
            {
                EndMagnet();
            }
        }
    }

    public void RegisterListeners()
    {
        gd.gotMagnet.AddListener(StartMagnet);
        gd.gotShield.AddListener(StartShield);
    }

    void StartShield()
    {
        gd.shieldInAct = true;
        shieldTimer = gd.ShieldTime;
        wc.workers.StartShieldPowerup();
    }

    void StartMagnet()
    {
        gd.magnetInAct = true;
        magnetTimer = gd.MagnetTime;
        wc.workers.StartMagnetPowerup();
    }

    void EndShield()
    {
        gd.shieldInAct = false;
        wc.workers.EndShieldPowerup();
    }

    void EndMagnet()
    {
        gd.magnetInAct = false;
        wc.workers.EndMagnetPowerup();
        gd.gotMagnetNoMore.Invoke();
    }
}
