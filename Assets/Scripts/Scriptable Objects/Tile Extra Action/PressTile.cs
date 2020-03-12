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

public enum Press
{
    None,
    Upper,
    Lower,
    Duel
}

public class PressTile : TileExtraAction
{
    static float lastCallTime;
    static Press lastPress;
    static bool inPress;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override IEnumerator TakeActionCoroutine()
    {
        yield return base.TakeActionCoroutine();

        yield return new WaitWhile(() => inPress);
        inPress = true;

        Press press = SetPress();
        // Set press value based on the order of the call
        if (Time.realtimeSinceStartup - lastCallTime > 3)
        {
            lastPress = press = ExtensionMethods.RandomEnumValue<Press>();
            Debug.LogWarning("1: " + lastPress);
        }
        else
        {
            press = SetPress();
            Debug.LogWarning("2: " + press);
        }

        lastCallTime = Time.realtimeSinceStartup;

        PlaySound(press);
        tileMover.Anim.SetTrigger(press.ToString() + "Press");

        actionInitiated = true;
        inPress = false;
    }

    Press SetPress()
    {
        Press press = Press.None;
        switch (lastPress)
        {
            case Press.None:
                press = Press.Duel;
                break;
            case Press.Upper:
                press = Press.Upper;
                break;
            case Press.Lower:
                press = Press.Lower;
                break;
        }
        return press;
    }

    void PlaySound(Press press)
    {
        switch (press)
        {
            case Press.Upper:
                AudioManager.Instance.PlaySound("Hydraullic press 3");
                break;
            case Press.Lower:
                AudioManager.Instance.PlaySound("Hydraullic press 2");
                break;
            case Press.Duel:
                AudioManager.Instance.PlaySound("Hydraullic press 1");
                break;
        }
    }
}
