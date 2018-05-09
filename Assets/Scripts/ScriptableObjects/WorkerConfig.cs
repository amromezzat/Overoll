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
    [HideInInspector]
    public UnityEvent onLeft;
    [HideInInspector]
    public UnityEvent onRight;
    [HideInInspector]
    public UnityEvent onJump;
    [HideInInspector]
    public UnityEvent onSlide;

    [Header("Jump Attributes")]
    public int jumpSpeed = 20;
    public float gravityFactor = 20;
    public float groundLevel = 0.25f;//worker y position

    [Header("Moving Attributes")]
    public int turnSpeed = 15;

    [Header("Crowd Behavior")]
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

    [Header("Worker Prefab")]
    public GameObject workerPrefab;

    private void OnEnable()
    {
        workers = new List<GameObject>();
        workersRb = new List<Rigidbody>();
    }
}
