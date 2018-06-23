﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkersManager : MonoBehaviour
{
    public WorkerFSM leader;
    public Button addWorkerBtn;
    public WorkerConfig wc;
    public TileConfig tc;
    public GameData gData;
    public int wPFactor = 2;

    void Awake()
    {
        gData.CoinCount = 0;
        wc.leader = leader;
        wc.workers.Add(leader);
        wc.onLeaderDeath.AddListener(LeaderDied);
        wc.onMergeOver.AddListener(MergingDone);
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

    public void AddWorker()
    {
        GameObject worker = ObjectPooler.instance.GetFromPool(wc.worker);
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(tc.disableSafeDistance + 5, tc.disableSafeDistance + 8);
        worker.transform.position = new Vector3(newXPos, worker.transform.position.y, newZPos);
        WorkerFSM workerFSM = worker.GetComponent<WorkerFSM>();
            
        wc.workers.Add(workerFSM);
        gData.CoinCount -= gData.workerPrice;

        if (gData.shieldInAct)
        {
            workerFSM.health = 1000;


        }
        
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
