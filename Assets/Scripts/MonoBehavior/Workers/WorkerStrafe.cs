using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerStrafe : MonoBehaviour, iHalt
{
    public LanesDatabase lanes;
    public WorkerConfig wc;
    public GameData gameData;

    public float strafeDuration = 0.1f;

    Animator animator;
    bool strafing = false;
    float strafeTimer = 0;

    private void Awake()
    {
        RegisterListeners();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (strafing)
        {
            float completedPortion = strafeTimer / strafeDuration;
            float exponentialPortion = completedPortion * completedPortion;
            float xPos = Mathf.Lerp(transform.position.x, lanes.CurrentLane.laneCenter, exponentialPortion);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            strafeTimer += Time.deltaTime;
            if (exponentialPortion >= 1)
            {
                strafing = false;
                animator.SetBool("StrafeRightAnim", false);
                animator.SetBool("StrafeLeftAnim", false);
            }
        }
    }

    void StrafeRight()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        if (!strafing)
        {
            strafeTimer = 0;
            animator.SetBool("StrafeRightAnim", true);
            lanes.GoRight();
            strafing = true;
        }
    }

    void StrafeLeft()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        if (!strafing)
        {
            strafeTimer = 0;
            animator.SetBool("StrafeLeftAnim", true);
            lanes.GoLeft();
            strafing = true;
        }
    }

    public void Halt()
    {
        wc.onLeft.RemoveListener(StrafeLeft);
        wc.onRight.RemoveListener(StrafeRight);
    }

    public void Resume()
    {
        wc.onLeft.AddListener(StrafeLeft);
        wc.onRight.AddListener(StrafeRight);
    }

    public void RegisterListeners()
    {
        gameData.OnStart.AddListener(Begin);
        gameData.onPause.AddListener(Halt);
        gameData.OnResume.AddListener(Resume);
        gameData.onEnd.AddListener(End);
    }

    public void Begin()
    {
        wc.onLeft.AddListener(StrafeLeft);
        wc.onRight.AddListener(StrafeRight);
    }

    public void End()
    {
        wc.onLeft.RemoveListener(StrafeLeft);
        wc.onRight.RemoveListener(StrafeRight);
    }
}
