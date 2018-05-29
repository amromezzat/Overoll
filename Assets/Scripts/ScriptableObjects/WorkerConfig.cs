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
    public float maxFormForce = 10;
    public float maxSpeed = 10;
    public float aheadFollowPoint = 5;//distance infront of leader for workers to follow
    [Header("Workers Formation")]
    public AnimationCurve rightWingShape;
    public AnimationCurve centerShape;
    public AnimationCurve leftWingShape;

    [HideInInspector]
    public List<GameObject> workers;
    [HideInInspector]
    public List<GameObject> hrWorkers;
    [HideInInspector]
    public List<GameObject> managerWorkers;
   
    [Header("Workers Types")]
    public PoolableType worker;
    public PoolableType HR;
    public PoolableType Manager;

    private void OnEnable()
    {
        workers = new List<GameObject>();
    }
}
