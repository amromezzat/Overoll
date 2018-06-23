using System.Collections;
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
    public Vector3 ExtraVelocity = Vector3.zero;

    Animator mAnim;
    Animator[] animList;
    IEnumerator slowingCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        RegisterListeners();
        slowingCoroutine = KillSpeed();
        mAnim = GetComponent<Animator>();
        if(mAnim == null)
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
            StartCoroutine(slowingCoroutine);
        }
    }

    //take extra speed when reaching workers;
    //object stays static relative to other objects
    //until it is close enough to workers
    IEnumerator TakeExtraSpeed()
    {
        float waitingTime = gameData.Speed / transform.position.z;
        yield return new WaitForSeconds(waitingTime);
        rb.velocity += ExtraVelocity;
    }

    void MoveObj()
    {
        rb.velocity = Vector3.back * gameData.Speed;
        if (isActiveAndEnabled && rb.velocity.magnitude > 0)
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
        rb.velocity = Vector3.zero;
    }
    
    public IEnumerator KillSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameData.slowingRate);
            rb.velocity = Vector3.back * gameData.Speed;
            SetAnimatorsSpeed(gameData.Speed / gameData.oldSpeed);
        }
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
        if (!isActiveAndEnabled)
            return;
        MoveObj();
        StopCoroutine(slowingCoroutine);
    }

    public void SlowDown()
    {
        if (!isActiveAndEnabled)
            return;
        rb.velocity = Vector3.back * gameData.Speed;
        StartCoroutine(slowingCoroutine);
        SetAnimatorsSpeed(gameData.Speed / gameData.oldSpeed);
    }
}
