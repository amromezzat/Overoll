using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerStrafe : MonoBehaviour, iHalt
{
    public LanesDatabase lanes;
    public WorkerConfig wc;
    public float strafeDuration = 0.5f;

    Rigidbody rb;
    Animator animator;

    bool strafing = false;
    float strafeTimer = 0;

    public GameData gameData;

    private void OnEnable()
    {
        RegisterListeners();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        gameData.OnStart.AddListener(Halt);
        gameData.onPause.AddListener(Halt);
        gameData.OnResume.AddListener(Resume);
    }

}
