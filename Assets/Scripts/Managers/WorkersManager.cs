using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkersManager : MonoBehaviour
{
    public GameObject leader;
    public Button myButton;
    public WorkerConfig wc;
    public TileConfig tc;
    public GameData gData;
    public int workerPrice;
    public int wPFactor = 2;

    void Start()
    {
        gData.CoinCount = 0;
        gData.workersNum = 1;
        wc.leader = leader;
    }

    void Update()
    {
        wc.aheadFollowPoint = gData.workersNum * (-0.35f);
        workerPrice = gData.workersNum * wPFactor;

        if (workerPrice > gData.CoinCount)
        {
            myButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            myButton.GetComponent<Button>().interactable = true;
        }
        if (leader.GetComponent<WorkerLifeCycle>().healthState == HealthState.Wrecked)
        {
            if (gData.workersNum > 0)
            {
                ElectNewLeader();
                leader.transform.position = Vector3.Lerp(leader.transform.position, new Vector3(0, 0.25f, 0), Time.deltaTime);
            }
            else
            {
                gData.gameState = GameState.GameOver;
                gData.onEnd.Invoke();
            }
        }
    }

    public void AddWorker()
    {
        GameObject worker = ObjectPooler.instance.GetFromPool(wc.worker);
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(tc.disableSafeDistance + 5, tc.disableSafeDistance + 8);
        worker.transform.position = new Vector3(newXPos, worker.transform.position.y, newZPos);
        gData.workersNum += 1;
        gData.CoinCount -= workerPrice;
    }

    public void ElectNewLeader()
    {
        wc.leader.GetComponent<WorkerStrafe>().enabled = false;
        wc.leader.GetComponent<PositionWorker>().enabled = true;
        wc.workers.Remove(leader);
        wc.leader = wc.workers[0];
        wc.leader.GetComponent<WorkerStrafe>().enabled = true;
        wc.leader.GetComponent<PositionWorker>().enabled = false;
    }
}
