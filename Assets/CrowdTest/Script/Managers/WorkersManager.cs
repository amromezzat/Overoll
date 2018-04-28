using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour
{
    public static WorkersManager instance = null;
    public GameObject leader;
    public static Rigidbody leaderRb;
    public static int laneWidth = 5; //width of each seperate lane
    public static int workersSepDis = 5;//distance workers keep from each other
    public static int arrivalSlowingRad = 5;//slow when entering this rad
    public static int maxSepForce = 10;
    public static int maxFolForce = 10;
    public static int maxSpeed = 10;
    public static int aheadFollowPoint = 5;//distance infront of workers to follow
    public static List<GameObject> workers;
    public static List<Rigidbody> workersRb = new List<Rigidbody>();

    private void Awake()
    {
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        leaderRb = leader.GetComponent<Rigidbody>();
        workers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Worker"));
        //workers rigidbody mostly won't be used(maybe removed soon)
        foreach (GameObject worker in workers)
        {
            workersRb.Add(worker.GetComponent<Rigidbody>());
        }
        leaderRb.velocity = new Vector3(5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeaderPos();

        //used for testing at this point for WorkersManager update should be empt
        if (leader.transform.position.x > 100)
        {
            Vector3 newPos = leader.transform.position;
            newPos.x = -100;
            leader.transform.position = newPos;
            SetWorkersNewPos();
        }
    }

    void SetWorkersNewPos()
    {

    }

    private void FixedUpdate()
    {

    }

    void UpdateLeaderPos()
    {
    }

}
