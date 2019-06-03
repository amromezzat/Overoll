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
public class PowerUpManager : MonoBehaviour, IHalt
{
    static PowerUpManager instance;
    public static PowerUpManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PowerUpManager>();

            return instance;
        }
    }

    public PowerUpVariable shield;
    public PowerUpVariable magnet;
    public PowerUpVariable teacup;
    public PowerUpVariable doublecoin;

    public WorkerConfig wc;
    //float magnetTimer;
    //float shieldTimer;
    //float shieldTime;

    public Slider DoubleCoin_Slider;
    public Image DoubleCoin_Image;

    public Slider Shield_Slider;
    public Image Shield_Image;

    public Slider Magnet_Slider;
    public Image Magnet_Image;

    public Slider TeaCup_Slider;
    public Image TeaCup_Image;

    private void Awake()
    {
        RegisterListeners();
        DisablePowerups();

        shield.BeginAction.AddListener(StartShield);
        shield.EndAction.AddListener(EndShield);
        magnet.BeginAction.AddListener(StartMagnet);
        magnet.EndAction.AddListener(EndMagnet);
        teacup.BeginAction.AddListener(StartTeaCup);
        teacup.EndAction.AddListener(EndTeaCup);
        doublecoin.BeginAction.AddListener(StartDoubleCoin);
        doublecoin.EndAction.AddListener(EndDoubleCoin);
    }

    void PausePowerups(bool val)
    {
        shield.paused = val;
        magnet.paused = val;
        teacup.paused = val;
        doublecoin.paused = val;
    }

    void DisablePowerups()
    {
        Shield_Slider.gameObject.SetActive(false);
        Magnet_Slider.gameObject.SetActive(false);
        TeaCup_Slider.gameObject.SetActive(false);
        DoubleCoin_Slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        Shield_Slider.value = Mathf.Lerp(0, 1, shield.ScaledTime);
        Shield_Image.fillAmount = Shield_Slider.value;
        Magnet_Slider.value = Mathf.Lerp(0, 1, magnet.ScaledTime);
        Magnet_Image.fillAmount = Magnet_Slider.value;
        TeaCup_Slider.value = Mathf.Lerp(0, 1, teacup.ScaledTime);
        TeaCup_Image.fillAmount = TeaCup_Slider.value;
        DoubleCoin_Slider.value = Mathf.Lerp(0, 1, doublecoin.ScaledTime);
        DoubleCoin_Image.fillAmount = DoubleCoin_Slider.value;
      
    }
     void StartShield ()
     {
        Shield_Slider.gameObject.SetActive(true);
        Shield_Slider.transform.SetAsLastSibling();
    }
    void EndShield()
    {
        Shield_Slider.gameObject.SetActive(false);
    }
    void StartMagnet()
    {
        Magnet_Slider.gameObject.SetActive(true);
        Shield_Slider.transform.SetAsLastSibling();
    }
    void EndMagnet()
    {
        Magnet_Slider.gameObject.SetActive(false);

    }
    void StartTeaCup()
    {
        TeaCup_Slider.gameObject.SetActive(true);
        Shield_Slider.transform.SetAsLastSibling();
    }
    void EndTeaCup()
    {
        TeaCup_Slider.gameObject.SetActive(false);

    }
    void StartDoubleCoin()
    {
        DoubleCoin_Slider.gameObject.SetActive(true);
        DoubleCoin_Slider.transform.SetAsLastSibling();
    }
     void EndDoubleCoin()
     {
        DoubleCoin_Slider.gameObject.SetActive(false);
     }

    public void Begin()
    {
        PausePowerups(false);
    }

    public void Halt()
    {
        PausePowerups(true);
    }

    public void Resume()
    {
        PausePowerups(false);
    }

    public void End()
    {
        DisablePowerups();
    }

    public void RegisterListeners()
    {
        GameManager.Instance.OnStart.AddListener(Begin);
        GameManager.Instance.onPause.AddListener(Halt);
        GameManager.Instance.OnResume.AddListener(Resume);
        GameManager.Instance.onEnd.AddListener(End);
    }
}
