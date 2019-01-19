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

public class EnvGenerator : MonoBehaviour, IHalt
{
    public TileConfig tc;
    public GameData gameData;

    EnvPooler pool;
    Transform lastTile;

    bool isHalt;

    private void OnEnable()
    {
        isHalt = true;
    }

    void Start()
    {
        RegisterListeners();
        pool = gameObject.GetComponent<EnvPooler>();
        lastTile = transform;

    }

    private void Update()
    {
        if (!isHalt && pool.activeTileCount < 8)
        {
            GenerateTile();
        }
    }

    // Generate an environment segment directly after last segment
    void GenerateTile()
    {
        var obj = pool.GetObjectFromPool();
        Vector3 objPos = obj.transform.position;
        objPos.z = lastTile.transform.GetTransformEnd();
        obj.transform.position = objPos;
        lastTile = obj.transform;
    }

    public void RegisterListeners()
    {
         GameManager.Instance.OnStart.AddListener(Begin);
         GameManager.Instance.onPause.AddListener(Halt);
         GameManager.Instance.OnResume.AddListener(Resume);
         GameManager.Instance.onEnd.AddListener(End);
    }

    public void Begin()
    {
        isHalt = false;
    }

    public void Halt()
    {
        isHalt = true;
    }

    public void Resume()
    {
        isHalt = false;
    }

    public void End()
    {
        isHalt = true;
    }

    public bool drawGizmos = false;
    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Color(0, 1, 0, 0.5F);
            Gizmos.DrawCube(transform.position, new Vector3(10, 10, 1));
        }
    }
}
