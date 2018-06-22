using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WorkerConfig", menuName = "Config/WorkerConfig")]
public class WorkerConfig : ScriptableObject
{

    public WorkerFSM leader;

    //Events available for other classes to register to
    [HideInInspector]
    public UnityEvent onLeft;
    [HideInInspector]
    public UnityEvent onRight;
    [HideInInspector]
    public UnityEvent onJump;
    [HideInInspector]
    public UnityEvent onSlide;
    [HideInInspector]
    public UnityEvent onLeaderDeath;
    [HideInInspector]
    public UnityEvent onMergeOver;



    [Header("Jump Slide Attributes")]
    public float jumpDuration = 1;
    public float slideDuration = 1;
    public float jumpHeight = 2;
    public float groundLevel = 0.25f;//worker y position

    [Header("Moving Attributes")]
    public float strafeDuration = 0.1f;
    public float takeLeadDuration = 1;

    [Header("Crowd Behavior")]
    public float workersSepDis = 0.8f;//distance workers keep from each other
    public float arrivalSlowingRad = 1;//slow when entering this rad
    public float maxSepForce = 30;
    public float maxFolForce = 10;
    public float maxSpeed = 10;
    [HideInInspector]
    public float aheadFollowPoint = 5;//distance infront of leader for workers to follow

    public WorkerList workers;
    [HideInInspector]
    public List<GameObject> hrWorkers;
    [HideInInspector]
    public List<GameObject> managerWorkers;

    [Header("Workers Types")]
    public PoolableType worker;
    public PoolableType HR;
    public PoolableType Manager;

    [Header("Worker Merge")]
    public int workersPerLevel = 5;
    public int levelsNum = 5;

    private void OnEnable()
    {
        workers = new WorkerList(workersPerLevel, levelsNum);
    }
}
