using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public Lanes lanes;
    public WorkerConfig wc;

    Rigidbody rb;
    bool turningRight = false;
    bool turningLeft = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        StopTurning();
    }

    void MoveLeft()
    {
        if (!turningRight && !turningLeft)
        {
            lanes.GoLeft();
            rb.velocity += wc.turnSpeed * Vector3.left;
            turningLeft = true;
        }
    }

    void MoveRight()
    {
        if (!turningRight && !turningLeft)
        {
            lanes.GoRight();
            rb.velocity += wc.turnSpeed * Vector3.right;
            turningRight = true;
        }
    }

    //when worker reaches lane center make him stick to it
    void StopTurning()
    {
        if ((turningRight && lanes.CurrentLane.laneCenter < transform.position.x)
    || (turningLeft && lanes.CurrentLane.laneCenter > transform.position.x))
        {
            //set within platfrom height from equation platformHeigt(at x pos)
            Vector3 newPos = transform.position;
            Vector3 newVel = rb.velocity;
            newPos.x = lanes.CurrentLane.laneCenter;
            transform.position = newPos;
            newVel.x = 0;
            rb.velocity = newVel;
            turningRight = false;
            turningLeft = false;
        }
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
