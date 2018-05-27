using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerController : MonoBehaviour
{
    public LanesDatabase lanes;
    public WorkerConfig wc;

    Rigidbody rb;
    bool turningRight = false;
    bool turningLeft = false;
    float turnT0;//turn start time
    Animator animator;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (turningRight)
        {
            if (lanes.CurrentLane.laneCenter < transform.position.x)
            {
                StopTurning();
                animator.SetBool("StrafeRightAnim", false);
            }
        }
        else if (turningLeft)
        {
            if (lanes.CurrentLane.laneCenter  > transform.position.x)
            {
                StopTurning();
                animator.SetBool("StrafeLeftAnim", false);
            }
        }
    }

    void MoveLeft()
    {
        if (!turningRight && !turningLeft)
        {
            animator.SetBool("StrafeLeftAnim", true);
            lanes.GoLeft();
            rb.AddForce(wc.turnSpeed * Vector3.left, ForceMode.Impulse);
            turningLeft = true;
            turnT0 = Time.time;
        }
    }

    void MoveRight()
    {
        if (!turningRight && !turningLeft)
        {
            animator.SetBool("StrafeRightAnim", true);
            lanes.GoRight();
            rb.AddForce(wc.turnSpeed * Vector3.right, ForceMode.Impulse);
            turningRight = true;
            turnT0 = Time.time;
        }
    }

    void StopTurning()
    {
        Vector3 newPos = transform.position;
        Vector3 newVel = rb.velocity;
        newPos.x = lanes.CurrentLane.laneCenter;
        transform.position = newPos;
        newVel.x = 0;
        rb.velocity = newVel;
        turningRight = false;
        turningLeft = false;
    }

    public void OnEnable()
    {
        wc.onLeft.AddListener(MoveLeft);
        wc.onRight.AddListener(MoveRight);
    }

    public void OnDisable()
    {
        wc.onLeft.RemoveListener(MoveLeft);
        wc.onRight.RemoveListener(MoveRight);
    }
}
