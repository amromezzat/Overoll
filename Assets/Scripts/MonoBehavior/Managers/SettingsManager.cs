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

public class LanguageTypeEvent : UnityEvent<LanguageTypes>
{

}

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public UnityEvent<LanguageTypes> ChangeLanguage = new LanguageTypeEvent();

    public LanguageTypes languageType;

    public Button LangBtn;
    public Sprite[] Langsprites;
    int clickCount=0;
    //public Sprite ARLang;
    //public Sprite ENLang;
    //public Sprite FRLang;
    //public Sprite DELang;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetLanguage(int val)
    {
        PlayerPrefs.SetInt("Language",val);
        OnChangeLanguage((LanguageTypes)val);
    }

    public void OnChangeLanguage(LanguageTypes langType)
    {
        languageType = langType;
        
        Debug.Log(languageType);

        ChangeLanguage.Invoke(languageType);
    }
     
    public void OnClickLangBTN()
    {
        if (clickCount == Langsprites.Length )
        {
            clickCount = 0;
            LangBtn.image.sprite = Langsprites[0];
            
        }
        LangBtn.image.sprite = Langsprites[clickCount];

   

        switch(clickCount)
        {
            case 0:
                OnChangeLanguage(LanguageTypes.AR);
                break;
            case 1:
                OnChangeLanguage(LanguageTypes.EN);
                break;
            case 2:
                OnChangeLanguage(LanguageTypes.DE);
                break;
            case 3:
                OnChangeLanguage(LanguageTypes.FR);
                break;
            default:
                OnChangeLanguage(LanguageTypes.EN);

                break;
        }
        clickCount++;
    }

        
     
    

}
