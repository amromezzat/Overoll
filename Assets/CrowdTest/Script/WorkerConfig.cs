using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WorkerConfig", menuName = "Config/WorkerConfig")]
public class WorkerConfig : ScriptableObject {
    [HideInInspector]
    public GameObject leader;
    [HideInInspector]
    public Rigidbody leaderRb;

    //Events available for other classes to register to
    public UnityEvent onLeft;
    public UnityEvent onRight;
    public UnityEvent onJump;
    public UnityEvent onSlide;

    public int jumpSpeed = 15;
    public int turnSpeed = 15;
    public float gravityFactor = 10;

    public float workersSepDis = 5;//distance workers keep from each other
    public float arrivalSlowingRad = 5;//slow when entering this rad
    public float maxSepForce = 10;
    public float maxFolForce = 10;
    public float maxSpeed = 10;
    public float aheadFollowPoint = 5;//distance infront of workers to follow

    [HideInInspector]
    public List<GameObject> workers;
    [HideInInspector]
    public List<Rigidbody> workersRb;

    private void OnEnable()
    {
        workers = new List<GameObject>();
        workersRb = new List<Rigidbody>();
    }
}
