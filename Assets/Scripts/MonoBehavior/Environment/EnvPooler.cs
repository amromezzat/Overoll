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
/// Move the environment around the player
/// </summary>

public class EnvPooler: MonoBehaviour
{
    public List<GameObject> listOfPrefabs;
    public int poolSize;
    //[HideInInspector]
    public List<GameObject> pool;
    public int activeTileCount = 0;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddToPool();
        }
    }

    public GameObject GetObjectFromPool()
    {
        activeTileCount++;
        {
            if (pool.Count == 0)
            {
                AddToPool();

            }

            GameObject element = pool[Random.Range(0, pool.Count)];
            element.SetActive(true);
            pool.Remove(element);
            return element;
        }
    }

    public void ReturnObjectToPool(GameObject returnedObj)
    {
        activeTileCount--;
        returnedObj.transform.position = gameObject.transform.position;
        returnedObj.SetActive(false);
        pool.Add(returnedObj);
    }

    void AddToPool()
    {
        int index = Random.Range(0, listOfPrefabs.Count);
        GameObject obj = Instantiate(listOfPrefabs[index], gameObject.transform);
        obj.SetActive(false);
        pool.Add(obj);

    }

}