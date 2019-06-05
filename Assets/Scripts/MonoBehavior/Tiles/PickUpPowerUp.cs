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

/// <summary>
/// This class is on powerups objects.
/// </summary>
public class PickUpPowerUp : MonoBehaviour
{
    ObjectReturner cReturn;

    void OnEnable()
    {
        cReturn = GetComponent<ObjectReturner>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Worker")
        {
            AudioManager.Instance.PlaySound("Power Up");
            if (tag == "Magnet")
            {
                PowerUpManager.Instance.magnet.StartPowerUp();
            }
            if (tag == "Vest")
            {
                PowerUpManager.Instance.shield.StartPowerUp();
            }
            if (tag == "TeaCup")
            {
                PowerUpManager.Instance.teacup.StartPowerUp();
            }
            if (tag == "x2")
            {
                PowerUpManager.Instance.doublecoin.StartPowerUp();
            }

            StartCoroutine(cReturn.ReturnToPool(0));
        }
    }
}
