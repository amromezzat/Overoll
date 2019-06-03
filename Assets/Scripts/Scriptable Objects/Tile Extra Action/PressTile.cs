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

[CreateAssetMenu(fileName = "Press", menuName = "AbstractFields/Extra Action Tiles/Press Tile")]
public class PressTile : TileExtraAction
{
    float lastCallTime;
    Press lastPress;

    private void OnEnable()
    {
        lastPress = ExtensionMethods.RandomEnumValue<Press>();
        lastCallTime = Time.realtimeSinceStartup;
    }

    protected override IEnumerator Action(TileMover caller)
    {
        yield return new WaitUntil(() => SpeedManager.Instance.speed.Value > 0.001f);

        float waitingTime = (caller.transform.position.z - relActivPos) / SpeedManager.Instance.speed.Value;

        // Wait for the object to be close to the player
        while (waitingTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitWhile(() => GameManager.Instance.gameState == GameState.Pause);
            waitingTime -= 0.1f;
        }

        AudioManager.Instance.PlaySound("Hydraullic press 1");

        Press press = Press.None;
        // Set press value based on the order of the call
        if (Time.realtimeSinceStartup - lastCallTime > 3)
            press = lastPress = ExtensionMethods.RandomEnumValue<Press>();
        else
            SetNextPress(ref press);

        lastCallTime = Time.realtimeSinceStartup;

        caller.Anim.SetTrigger(press.ToString() + "Press");
    }

    void SetNextPress(ref Press press)
    {
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
            case Press.Duel:
                press = Press.None;
                break;
        }
    }
}
