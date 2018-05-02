using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public Lanes lanes;
    public WorkerConfig wc;
    public int jumpSpeed = 15;
    public int turnSpeed = 15;
    public float gravityFactor = 10;
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
        TheInputManager theInputManger = new TheInputManager();
        theInputManger.OnJump.AddListener(Jump);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
    }

    void Jump()
    {
        //only jump if it is not yet currently jumping
        if (!jumping)
        {
            rb.velocity += Vector3.up * jumpSpeed;
            jumping = true;
            jumpt0 = Time.time;
        }
    }

    void MoveLeft()
    {
        if (!turningRight && !turningLeft)
        {
            lanes.GoLeft();
            rb.velocity += turnSpeed * Vector3.left;
            turningLeft = true;
            turnt0 = Time.time;
        }
    }

    void MoveRight()
    {
        if (!turningRight && !turningLeft)
        {
            lanes.GoRight();
            rb.velocity += turnSpeed * Vector3.right;
            turningRight = true;
            turnt0 = Time.time;
        }
    }

    void FixedUpdate()
    {
        newPos = transform.position;
        newVel = rb.velocity;
        if (jumping)
        {

            newVel.y -= gravityFactor * (Time.time - jumpt0);
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
}
