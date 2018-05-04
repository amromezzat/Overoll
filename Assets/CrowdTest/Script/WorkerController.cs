using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public Lanes lanes;
    public WorkerConfig wc;

    Rigidbody rb;

    bool jumping = false;
    bool turningRight = false;
    bool turningLeft = false;
    float jumpt0;//jump start time
    float turnt0;//turn start time

    Vector3 newVel;
    Vector3 newPos;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Jump()
    {
        //only jump if it is not yet currently jumping
        if (!jumping)
        {
            rb.velocity += Vector3.up * wc.jumpSpeed;
            jumping = true;
            jumpt0 = Time.time;
        }
    }

    void MoveLeft()
    {
        if (!turningRight && !turningLeft)
        {
            lanes.GoLeft();
            rb.velocity += wc.turnSpeed * Vector3.left;
            turningLeft = true;
            turnt0 = Time.time;
        }
    }

    void MoveRight()
    {
        if (!turningRight && !turningLeft)
        {
            lanes.GoRight();
            rb.velocity += wc.turnSpeed * Vector3.right;
            turningRight = true;
            turnt0 = Time.time;
        }
    }

    void Slide() {
        print("sliding");
    }

    //applying gravitational force to the body
    void StopJumping()
    {
        if (jumping)
        {

            newVel.y -= wc.gravityFactor * (Time.time - jumpt0);
            rb.velocity = newVel;
            // And test that the character is not on the ground again.
            //calculate platform height from equation platformHeigt(at x pos)
            if (transform.position.y < 0)
            {
                //set within platfrom height from equation platformHeigt(at x pos)
                newPos.y = 0;
                transform.position = newPos;
                newVel.y = 0;
                rb.velocity = newVel;
                jumping = false;
            }
        }
    }

    //when worker reaches lane center make him stick to it
    void StopTurning()
    {
        if ((turningRight && lanes.CurrentLane.laneCenter < transform.position.x)
    || (turningLeft && lanes.CurrentLane.laneCenter > transform.position.x))
        {
            //set within platfrom height from equation platformHeigt(at x pos)
            newPos.x = lanes.CurrentLane.laneCenter;
            transform.position = newPos;
            newVel.x = 0;
            rb.velocity = newVel;
            turningRight = false;
            turningLeft = false;
        }
    }

    void FixedUpdate()
    {
        newPos = transform.position;
        newVel = rb.velocity;

        StopJumping();
        StopTurning();
    }

    public void OnEnable()
    {
        wc.onJump.AddListener(Jump);
        wc.onSlide.AddListener(Slide);
        wc.onLeft.AddListener(MoveLeft);
        wc.onRight.AddListener(MoveRight);
    }

    public void OnDisable()
    {
        wc.onJump.RemoveListener(Jump);
        wc.onSlide.RemoveListener(Slide);
        wc.onLeft.RemoveListener(MoveLeft);
        wc.onRight.RemoveListener(MoveRight);
    }
}
