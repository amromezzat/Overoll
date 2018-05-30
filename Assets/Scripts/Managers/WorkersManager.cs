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
    public int wPFactor = 20;
    public int wPFactor = 2;

    private void OnEnable()
    {
        leader.GetComponent<WorkerStrafe>().enabled = true;
        leader.GetComponent<PositionWorker>().enabled = false;
    }

    void Start()
    {
        gData.CoinCount = 0;
        gData.workersNum = 1;
        wc.leader = leader;
        wc.leaderRb = leader.GetComponent<Rigidbody>();
        //wc.leaderRb.velocity = new Vector3(10, 0);
    }

    void Update()
    {
        workerPrice = gData.workersNum * wPFactor;

        if (workerPrice > gData.CoinCount)
        {
            myButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            myButton.GetComponent<Button>().interactable = true;
        }

        if (leader.GetComponent<WorkerHealth>().state == workerState.Dead && wc.workers.Count > 0)
        {
            ElectNewLeader();
            leader.transform.position = Vector3.Lerp(leader.transform.position, new Vector3(0, 0.25f, 0), Time.deltaTime);
        }
        else
        {
            gData.gameState = GameState.gameEnded;
            //triger event el end game
        }
    }

    public void AddWorker()
    {
        GameObject worker = ObjectPool.instance.GetFromPool(wc.worker);
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(leader.transform.position.z - tc.laneWidth, leader.transform.position.z + tc.laneWidth);
        float newZPos = Random.Range(leader.transform.position.z -1, leader.transform.position.z -5);
        worker.transform.position = new Vector3(newXPos, worker.transform.position.y, newZPos);
        gData.workersNum += 1;
        gData.CoinCount -= workerPrice;
    }

    public void ElectNewLeader()
    {
        leader.GetComponent<WorkerStrafe>().enabled = false;
        leader.GetComponent<PositionWorker>().enabled = true;
        wc.workers.Remove(leader);
        leader = wc.workers[0];
        leader.GetComponent<WorkerStrafe>().enabled = true;
        leader.GetComponent<PositionWorker>().enabled = false;
    }
}
