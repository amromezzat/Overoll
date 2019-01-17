/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

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
    public UnityEvent onAddWorker;//on double tap
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
    public PoolableType workerType;
    public PoolableType hrType;
    public PoolableType managerType;

    [Header("Worker Merge")]
    public int workersPerLevel = 5;
    public int levelsNum = 5;

    private void OnEnable()
    {
        workers = new WorkerList(workersPerLevel, levelsNum);
    }
}
