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
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// this is resposible for powerup ui
/// </summary>
public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    public PowerUpVariable shield;
    public PowerUpVariable magnet;
    public PowerUpVariable teacup;

    public WorkerConfig wc;
    //float magnetTimer;
    float shieldTimer;
    float shieldTime;
    

    public Slider Shield_Slider;
    public Image Shield_Image;
    private void Awake()
    {
        Shield_Slider.gameObject.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }

        
        shield.BeginAction.AddListener(StartShield);
        shield.EndAction.AddListener(EndShield);
       // magnet.BeginAction.AddListener(wc.workers.StartMagnetPowerup);
       // magnet.EndAction.AddListener(wc.workers.EndMagnetPowerup);
      //  teacup.BeginAction.AddListener(wc.workers.StartTeacupPowerUp);
      //  teacup.EndAction.AddListener(wc.workers.EndTeacupPowerUp);
       
    }
    private void Update()
    {
        Shield_Slider.value = Mathf.Lerp(0, 1, shield.ScaledTime);
        Shield_Image.fillAmount = Shield_Slider.value;
        //Debug.Log(shield.ScaledTime+"::"+Shield_Slider.value);
    }
     void StartShield ()
     {
        Shield_Slider.gameObject.SetActive(true);
    }
    void EndShield()
    {
        Shield_Slider.gameObject.SetActive(false);
    }

}
