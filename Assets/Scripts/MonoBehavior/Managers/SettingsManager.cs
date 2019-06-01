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

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LanguageTypeEvent : UnityEvent<LanguageType>
{

}

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public GameObject creditsPanel;
    public GameObject settingMenu;
    public UnityEvent<LanguageType> languageChanged = new LanguageTypeEvent();

    [SerializeField]
    LanguageType languageType;

    // Set Language for first time use with the device language
    // or if cache was cleared
    Dictionary<int, int> availableSystemLanguages = new Dictionary<int, int>() { { 1, 0 },{ 10, 1 },
        {15,2 },{14, 3 } };

    public LanguageType currentLanguage
    {
        set
        {
            value = (LanguageType)((int)value % Langsprites.Length);

            SetLanguage((int)value);
        }
        get
        {
            return languageType;
        }
    }

    public int currentLanguageIndex
    {
        set
        {
            value %= Langsprites.Length;

            SetLanguage(value);
        }
        get
        {
            return (int)languageType;
        }
    }

    public Button LangBtn;
    public Sprite[] Langsprites;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (PlayerPrefs.HasKey("Language"))
            currentLanguageIndex = PlayerPrefs.GetInt("Language");
        else
            currentLanguageIndex = availableSystemLanguages[(int)Application.systemLanguage];
    }

    public void OnClickCredits()
    {
        creditsPanel.gameObject.SetActive(true);
        settingMenu.gameObject.SetActive(false);
    }
    public void OnExitCredits()
    {
        settingMenu.gameObject.SetActive(true);
        creditsPanel.gameObject.SetActive(false);
    }
    public void SetLanguage(int index)
    {
        languageType = (LanguageType)index;
        LangBtn.image.sprite = Langsprites[index];
        PlayerPrefs.SetInt("Language", index);
        languageChanged.Invoke((LanguageType)index);
        Debug.Log((LanguageType)index);
    }

    [ContextMenu("Update in-game language")]
    public void UpdateLanguage()
    {
        currentLanguage = languageType;
    }

    public void OnClickLangBTN()
    {
        currentLanguageIndex++;
    }
}
