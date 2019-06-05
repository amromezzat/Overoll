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
using UnityEngine.UI;
using System.Collections.Generic;

public class WorkersManager : MonoBehaviour
{
    static WorkersManager instance;
    public static WorkersManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<WorkersManager>();

            return instance;
        }
    }

    public WorkerFSM leader;
    public Button addWorkerBtn;
    public WorkerConfig wc;
    public TileConfig tc;
    public int wPFactor = 2;

    public PowerUpVariable shield;
    public PowerUpVariable magnet;
    public PowerUpVariable teacup;
    public PowerUpVariable doublecoin;

    public bool DoubleCoinOn
    {
        get
        {
            return workers.doubleCoinOn;
        }
    }

    public int WorkersCount
    {
        get
        {
            return workers.Count;
        }
    }

    [HideInInspector]
    public WorkerList workers = new WorkerList(0, 0);

    void Awake()
    {
        workers = new WorkerList(wc.workersPerLevel, wc.levelsNum);

        wc.onAddWorker.RemoveAllListeners();
        wc.onLeaderDeath.RemoveAllListeners();
        wc.onMergeOver.RemoveAllListeners();

        shield.BeginAction.AddListener(workers.StartShieldPowerup);
        shield.EndAction.AddListener(workers.EndShieldPowerup);
        magnet.BeginAction.AddListener(workers.StartMagnetPowerup);
        magnet.EndAction.AddListener(workers.EndMagnetPowerup);

        teacup.BeginAction.AddListener(workers.StartTeacupPowerUp);
        teacup.EndAction.AddListener(workers.EndTeacupPowerUp);

        doublecoin.BeginAction.AddListener(workers.StartDoubleCoin);
        doublecoin.EndAction.AddListener(workers.EndDoubleCoin);

        workers.Add(leader);
    }

    private void Start()
    {
        wc.onLeaderDeath.AddListener(LeaderDied);
        wc.onMergeOver.AddListener(MergingDone);
        wc.onAddWorker.AddListener(OnDoubleTap);
    }

    void Update()
    {
        wc.aheadFollowPoint = -Mathf.Log10(workers.Count + 1) - 0.5f;
        ScoreManager.Instance.workerPrice = (workers.Count + 1) * wPFactor;

        if (ScoreManager.Instance.workerPrice > ScoreManager.Instance.coinsCount.Value)
        {
            addWorkerBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            addWorkerBtn.GetComponent<Button>().interactable = true;
        }
    }

    public void OnDoubleTap()
    {
        if (GameManager.Instance.gameState == GameState.Gameplay)
        {
            if (ScoreManager.Instance.workerPrice <= ScoreManager.Instance.coinsCount.Value)
            {
                AddWorker();
            }
        }
    }


    public void AddWorker(Vector2 pos)
    {
        if (TutorialManager.Instance.Active && TutorialManager.Instance.TutorialState == TutorialState.AddWorker)
            TutorialManager.Instance.ExitState();

        GameObject worker = ObjectPooler.Instance.GetFromPool(wc.workerType);
        worker.transform.position = new Vector3(pos.x, worker.transform.position.y, pos.y);
        WorkerFSM workerFSM = worker.GetComponent<WorkerFSM>();
        workerFSM.level = 0;
        workerFSM.health = 1;

        workers.Add(workerFSM);
        AudioManager.Instance.PlaySound("Add Worker");
    }

    public void AddWorker()
    {
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(-6, -3);
        AddWorker(new Vector2(newXPos, newZPos));

        if (!TutorialManager.Instance.Active)
            ScoreManager.Instance.DeductWorkerPrice();
    }

    public void RemoveWorker(WorkerFSM worker)
    {
        workers.Remove(worker);
    }

    public void Descend(WorkerFSM worker)
    {
        workers.Descend(worker);
    }

    void MergingDone()
    {
        workers.Ascend();
    }

    void LeaderDied()
    {
        if (workers.Count > 0)
        {
            ElectNewLeader();
        }
        else
        {
            GameManager.Instance.gameState = GameState.GameOver;
            GameManager.Instance.onEnd.Invoke();
            magnet.ResetPowerUp();
            teacup.ResetPowerUp();
            shield.ResetPowerUp();
            // Debug.Log("Dead");
        }
    }

    public void ElectNewLeader()
    {
        leader = workers.GetNewLeader();
    }
}
