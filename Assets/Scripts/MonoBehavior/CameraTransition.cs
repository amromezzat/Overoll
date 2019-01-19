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
using UnityEngine.Playables;

/// <summary>
/// Rotate the camera from directly facing the worker to 
/// gameplay view
/// </summary>
public class CameraTransition : MonoBehaviour, IHalt
{
    public GameData gd;
    public TileConfig tc;
    public GameObject startView;
    public GameObject backView;
    public GameObject ground;
    public Transform beginTrans;
    public Transform EndTrans;
    public float transTime = 2f;

    Transform current;
    Transform next;
    PlayableDirector playableDirector;
    float timer = 0;
    bool onHalt = true;
    Rigidbody startViewRB;

    private void OnEnable()
    {
        RegisterListeners();
        playableDirector = startView.GetComponent<PlayableDirector>();
        startViewRB = startView.GetComponent<Rigidbody>();
    }

    public void Begin()
    {
        timer = 0;
        onHalt = false;
        next = EndTrans;
        playableDirector.enabled = false;
        //startViewRB.velocity = Vector3.back * gd.Speed;
        startViewRB.velocity = Vector3.back * SpeedManager.Instance.speed.Value;
        ground.SetActive(false);
    }

    public void End()
    {

    }

    public void Halt()
    {
        onHalt = true;
        startViewRB.velocity = Vector3.zero;
    }

    public void RegisterListeners()
    {
        gd.OnStart.AddListener(Begin);
        gd.onPause.AddListener(Halt);
        gd.OnResume.AddListener(Resume);
        gd.onEnd.AddListener(End);
    }

    public void Resume()
    {
        onHalt = false;
        // startViewRB.velocity = Vector3.back * gd.Speed;
        startViewRB.velocity = Vector3.back * SpeedManager.Instance.speed.Value;
    }

    void Start()
    {
        current = beginTrans;
        next = beginTrans;
    }

    void Update()
    {
        if (onHalt)
        {
            return;
        }
        timer += Time.deltaTime;
        float completedPortion = timer / transTime;
        float sinPortion = Mathf.Sin(completedPortion * Mathf.PI / (transTime * 2));
        transform.position = Vector3.Lerp(current.position, next.position, sinPortion);
        transform.rotation = Quaternion.Lerp(current.rotation, next.rotation, sinPortion);
        if (completedPortion >= transTime)
        {
            Destroy(startView);
            enabled = false;
        }
    }

    private void OnDisable()
    {
        gd.OnStart.RemoveListener(Begin);
        gd.onPause.RemoveListener(Halt);
        gd.OnResume.RemoveListener(Resume);
        gd.onEnd.RemoveListener(End);
    }
}