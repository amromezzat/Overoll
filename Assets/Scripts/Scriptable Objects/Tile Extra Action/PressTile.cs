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
    TileMover firstCaller;
    Press press;

    private void OnEnable()
    {
        lastCallTime = Time.realtimeSinceStartup;
    }

    protected override IEnumerator Action(TileMover caller)
    {
        // Link callers in a certain time window
        float passedTime = Time.realtimeSinceStartup - lastCallTime;
        if(passedTime > 1)
        {
            firstCaller = caller;
            lastCallTime = Time.realtimeSinceStartup;
        }

        // Wait for the object to be close to the player
        float waitingTime = (caller.transform.position.z - relActivPos) / SpeedManager.Instance.speed.Value;
        yield return new WaitForSeconds(waitingTime);

        // Set press value based on the order of the call
        if (firstCaller == caller)
            press = ExtensionMethods.RandomEnumValue<Press>();
        else
            SetSecondPress();

        caller.Anim.SetTrigger(press.ToString() + "Press");
    }

    void SetSecondPress()
    {
        switch (press)
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
