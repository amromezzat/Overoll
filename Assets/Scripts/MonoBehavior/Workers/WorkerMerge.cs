using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMerge : MonoBehaviour
{
    public Transform followedTransform;

    //WorkerCollisionHandler workerLifeCycle;
    //PositionWorker positionWorker;


    // Use this for initialization
    void Awake()
    {
        //workerLifeCycle = GetComponent<WorkerCollisionHandler>();
        //positionWorker = GetComponent<PositionWorker>();
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
