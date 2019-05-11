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

public class LanguageTypeEvent : UnityEvent<LanguageTypes>
{

}

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public UnityEvent<LanguageTypes> ChangeLanguage = new LanguageTypeEvent();

    [SerializeField]
    LanguageTypes languageType;

    // Set Language for first time use with the device language
    // or if cache was cleared
    Dictionary<int, int> availableSystemLanguages = new Dictionary<int, int>() { { 1, 0 },{ 10, 1 },
        {15,2 },{14, 3 } };

    public LanguageTypes currentLanguage
    {
        set
        {
            SetLanguage(value);
            languageType = value;
        }
        get
        {
            return languageType;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (PlayerPrefs.HasKey("Language"))
            SetLanguage(PlayerPrefs.GetInt("Language"));
        else
            SetLanguage(availableSystemLanguages[(int)Application.systemLanguage]);
    }

    [ContextMenu("Update in-game language")]
    public void UpdateLanguage()
    {
        SetLanguage(languageType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val">0: Ar, 1:En, 2:Du, 3:Fr </param>
    public void SetLanguage(int val)
    {
        PlayerPrefs.SetInt("Language", val);

        Debug.Log((LanguageTypes)val);

        ChangeLanguage.Invoke((LanguageTypes)val);
    }

    public void SetLanguage(LanguageTypes language)
    {
        PlayerPrefs.SetInt("Language", (int)language);

        Debug.Log(languageType);

        ChangeLanguage.Invoke(languageType);
    }


}
