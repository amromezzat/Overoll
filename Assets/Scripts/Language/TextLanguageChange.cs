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
using RTLTMPro;

public class TextLanguageChange : MonoBehaviour,ILanguageChangable
{
    public RTLTextMeshPro targetText;

    public string[] StringList;

    LanguageTypes m_LangType;

    private void OnEnable()
    {
        targetText = GetComponent<RTLTextMeshPro>();
        //m_LangType = SettingsManager.Instance.languageType;
        m_LangType = (LanguageTypes)PlayerPrefs.GetInt("Language");
        targetText.text = StringList[(int)m_LangType];
    }

    private void Awake()
    {
       // Debug.Log(m_LangType);
        
    }
    public void ChangeLanguage(LanguageTypes lanType)
    {
        Debug.Log(lanType);
        Debug.Log("targetText" + targetText.text);
    }

    public void RegisterListners()
    {
        SettingsManager.Instance.ChangeLanguage.AddListener(ChangeLanguage);
    }
}
