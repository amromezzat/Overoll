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
    public GameState gstate;
    public int workerPrice;
    public int wPFactor= 20;


   void Start()
    {
        gstate.CoinCount = 0;
        gstate.workersNum = 1;
        wc.leader = leader;
        wc.leaderRb = leader.GetComponent<Rigidbody>();
        //wc.leaderRb.velocity = new Vector3(10, 0);
    }
    
    void Update()
    {
        workerPrice = gstate.workersNum * wPFactor;

        if (workerPrice > gstate.CoinCount)
        {
            myButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            myButton.GetComponent<Button>().interactable = true;
        }
    }


    public void AddWorker()
    {
        GameObject worker= ObjectPool.instance.GetFromPool(wc.worker);
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(leader.transform.position.z - tc.laneWidth, leader.transform.position.z + tc.laneWidth);
        worker.transform.position = new Vector3(newXPos, worker.transform.position.y, newZPos);
        gstate.workersNum += 1;
        gstate.CoinCount -= workerPrice;
            
    }
}
