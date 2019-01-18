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
using UnityEngine.UI;

public class WorkersManager : MonoBehaviour
{
    public static WorkersManager Instance;

    public WorkerFSM leader;
    public Button addWorkerBtn;
    public WorkerConfig wc;
    public TileConfig tc;
    public GameData gData;
    public int wPFactor = 2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        gData.CoinCount = 0;
        wc.leader = leader;
        wc.workers.Add(leader);
        wc.onLeaderDeath.AddListener(LeaderDied);
        wc.onMergeOver.AddListener(MergingDone);
        wc.onAddWorker.AddListener(doubleTap);
    }

    void Update()
    {
        wc.aheadFollowPoint = -Mathf.Log10(wc.workers.Count + 1) - 0.5f;
        gData.workerPrice = (wc.workers.Count + 1) * wPFactor;

        if (gData.workerPrice > gData.CoinCount)
        {
            addWorkerBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            addWorkerBtn.GetComponent<Button>().interactable = true;
        }
    }

    public void doubleTap()
    {
        if(gData.gameState == GameState.Gameplay)
        {
            if(gData.workerPrice < gData.CoinCount || gData.tutorialActive)
                AddWorker();
        }
    }

    public void AddWorker()
    {
        if (gData.tutorialActive && gData.TutorialState == TutorialState.AddWorker)
        {
            gData.onSpeedUp.Invoke();
        }
        GameObject worker = ObjectPooler.instance.GetFromPool(wc.workerType);
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(tc.disableSafeDistance + 5, tc.disableSafeDistance + 8);
        worker.transform.position = new Vector3(newXPos, worker.transform.position.y, newZPos);
        WorkerFSM workerFSM = worker.GetComponent<WorkerFSM>();
            
        wc.workers.Add(workerFSM);
        gData.CoinCount -= gData.workerPrice;     
    }

    void MergingDone()
    {
        wc.workers.Ascend();
    }

    void LeaderDied()
    {
        if (wc.workers.Count > 0)
        {
            ElectNewLeader();
        }
        else
        {
            gData.gameState = GameState.GameOver;
            gData.onEnd.Invoke();
        }
    }

    public void ElectNewLeader()
    {
        wc.leader = wc.workers.GetNewLeader();
    }
}
