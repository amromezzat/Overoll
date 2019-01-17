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
