using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMerge : MonoBehaviour
{
    public Transform followedTransform;

    WorkerLifeCycle workerLifeCycle;
    PositionWorker positionWorker;
    WorkerFollowState wfs;


    // Use this for initialization
    void Awake()
    {
        workerLifeCycle = GetComponent<WorkerLifeCycle>();
        positionWorker = GetComponent<PositionWorker>();
        wfs = GetComponent<WorkerFollowState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMerging(Transform _followedTransform)
    {
        followedTransform = _followedTransform;
    }
}
