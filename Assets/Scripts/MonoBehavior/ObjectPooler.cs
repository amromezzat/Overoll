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

public class ObjectPooler : MonoBehaviour
{
    static ObjectPooler instance;
    public static ObjectPooler Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ObjectPooler>();

            return instance;
        }
    }
    public PoolDatabase pd;

    [HideInInspector]
    public int segmentActiveCount = 0;

    Dictionary<PoolableType, Queue<GameObject>> poolDict;

    private void Awake()
    {
        poolDict = new Dictionary<PoolableType, Queue<GameObject>>(pd.poolableList.Count);

        foreach (PoolableObj po in pd.poolableList)
        {
            Queue<GameObject> instancesQueue = new Queue<GameObject>(po.count);
            for (int i = 0; i < po.count; i++)
            {
                GameObject newGameObj = InstantiateGameObj(po.prefab);
                instancesQueue.Enqueue(newGameObj);
            }
            poolDict[po.type] = instancesQueue;
        }
    }

    public GameObject GetFromPool(PoolableType instType)
    {
        if (poolDict.ContainsKey(instType))
        {
            Queue<GameObject> instQueue = poolDict[instType];
            if (instQueue.Count > 0)
            {
                GameObject pooledObj = instQueue.Dequeue();
                return pooledObj;
            }
            return InstantiateGameObj(pd[instType].prefab, true); ;
        }
        Debug.LogError("Instance is invalid: " + instType.name);
        return null;
    }

    public void ReturnToPool(PoolableType instType, GameObject inst)
    {
        if (!poolDict.ContainsKey(instType))
        {
            Debug.LogError("Instance is invalid", instType);
        }
        ObjectReturner instObjReturner = inst.GetComponent<ObjectReturner>();
        if (instObjReturner.inActiveSegment)
        {
            instObjReturner.inActiveSegment = false;
            segmentActiveCount--;
        }
        Queue<GameObject> instQueue = poolDict[instType];
        inst.SetActive(false);
        instQueue.Enqueue(inst);

    }

    public GameObject InstantiateGameObj(GameObject prefab, bool active = false)
    {
        GameObject newGameObj = Instantiate(prefab);
        newGameObj.SetActive(active);
        return newGameObj;
    }
}
