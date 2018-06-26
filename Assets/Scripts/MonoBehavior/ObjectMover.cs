﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class resposiple for moving the tile
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour, IHalt, IChangeSpeed
{
    public TileConfig tc;
    private Rigidbody rb;

    public GameData gameData;
    public float ExtraVelocity = 0;

    Animator mAnim;
    Animator[] animList;

    bool isKillingSpeed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        RegisterListeners();
        mAnim = GetComponent<Animator>();
        if (mAnim == null)
        {
            animList = GetComponentsInChildren<Animator>();
        }
    }

    private void OnEnable()
    {
        if (gameData.gameState == GameState.Gameplay)
        {
            MoveObj();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        if (gameData.tutorialActive && gameData.TutorialState != TutorialState.Null)
        {
            isKillingSpeed = true;
        }
    }

    void Update()
    {
        if (isKillingSpeed)
        {
            rb.velocity = Vector3.back * gameData.Speed;
            SetAnimatorsSpeed(gameData.Speed / gameData.oldSpeed);
        }
    }

    //take extra speed when reaching workers;
    //object stays static relative to other objects
    //until it is close enough to workers
    IEnumerator TakeExtraSpeed()
    {
        float waitingTime = transform.position.z / gameData.Speed;
        yield return new WaitForSeconds(waitingTime);
        rb.velocity += Vector3.back * ExtraVelocity;
        mAnim.speed = 1;
    }

    void MoveObj()
    {
        rb.velocity = Vector3.back * gameData.Speed;
        if (isActiveAndEnabled && ExtraVelocity > 0)
        {
            StartCoroutine(TakeExtraSpeed());
        }
        SetAnimatorsSpeed(1);
    }

    public void Halt()
    {
        rb.velocity = Vector3.zero;
        SetAnimatorsSpeed(0);
    }

    public void Resume()
    {
        MoveObj();
    }

    public void RegisterListeners()
    {
        gameData.OnStart.AddListener(Begin);
        gameData.onPause.AddListener(Halt);
        gameData.OnResume.AddListener(Resume);
        gameData.onEnd.AddListener(End);
        gameData.onSlowDown.AddListener(SlowDown);
        gameData.onSpeedUp.AddListener(SpeedUp);
    }

    public void Begin()
    {
        MoveObj();
    }

    public void End()
    {
        rb.velocity = Vector3.back * ExtraVelocity;
    }

    void SetAnimatorsSpeed(float speed)
    {
        if (mAnim != null)
        {
            mAnim.speed = speed;
        }
        else if (animList != null)
        {
            foreach (Animator anim in animList)
            {
                anim.speed = speed;
            }
        }
    }

    public void SpeedUp()
    {
        MoveObj();
        isKillingSpeed = false;
    }

    public void SlowDown()
    {
        isKillingSpeed = true;
    }
}
