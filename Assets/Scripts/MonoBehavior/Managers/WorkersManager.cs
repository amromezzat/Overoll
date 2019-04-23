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

public class WorkersManager : MonoBehaviour
{
    public static WorkersManager Instance;

    public WorkerFSM leader;
    public Button addWorkerBtn;
    public WorkerConfig wc;
    public TileConfig tc;
    public int wPFactor = 2;

    public PowerUpVariable shield;
    public PowerUpVariable magnet;
    public PowerUpVariable teacup;
    public PowerUpVariable doublecoin;

    //[HideInInspector]
    //public int hrNum;
    //[HideInInspector]
    //public int bossNum;
    //[HideInInspector]
    //public PoolableType leaderType;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        shield.BeginAction.AddListener(wc.workers.StartShieldPowerup);
        shield.EndAction.AddListener(wc.workers.EndShieldPowerup);
        magnet.BeginAction.AddListener(wc.workers.StartMagnetPowerup);
        magnet.EndAction.AddListener(wc.workers.EndMagnetPowerup);

        teacup.BeginAction.AddListener(wc.workers.StartTeacupPowerUp);
        teacup.EndAction.AddListener(wc.workers.EndTeacupPowerUp);

        doublecoin.BeginAction.AddListener(wc.workers.StartDoubleCoin);
        doublecoin.EndAction.AddListener(wc.workers.EndDoubleCoin);

        wc.leader = leader;
        wc.workers.Add(leader);
        wc.onLeaderDeath.AddListener(LeaderDied);
        wc.onMergeOver.AddListener(MergingDone);
        wc.onAddWorker.AddListener(OnDoubleTap);

        leader.gameObject.SetActive(true);
    }

    void Update()
    {
        wc.aheadFollowPoint = -Mathf.Log10(wc.workers.Count + 1) - 0.5f;
        ScoreManager.Instance.workerPrice = (wc.workers.Count + 1) * wPFactor;

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
            if (ScoreManager.Instance.workerPrice < ScoreManager.Instance.coinsCount.Value || TutorialManager.Instance.tutorialActive)
                AddWorker();
        }
    }


    public void AddWorker(Vector2 pos)
    {
        if (TutorialManager.Instance.tutorialActive && TutorialManager.Instance.TutorialState == TutorialState.AddWorker)
        {
            SpeedManager.Instance.ResetSpeed();
        }
        GameObject worker = ObjectPooler.instance.GetFromPool(wc.workerType);
        worker.transform.position = new Vector3(pos.x, worker.transform.position.y, pos.y);
        WorkerFSM workerFSM = worker.GetComponent<WorkerFSM>();

        wc.workers.Add(workerFSM);
    }

    public void AddWorker()
    {
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(tc.disableSafeDistance + 5, tc.disableSafeDistance + 8);
        AddWorker(new Vector2(newXPos, newZPos));
        ScoreManager.Instance.DeductWorkerPrice();
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
            GameManager.Instance.gameState = GameState.GameOver;
            GameManager.Instance.onEnd.Invoke();
            magnet.ResetPowerUp();
            teacup.ResetPowerUp();
            shield.ResetPowerUp();
            Debug.Log("Dead");
        }
    }

    public void ElectNewLeader()
    {
        wc.leader = wc.workers.GetNewLeader();
    }
}
