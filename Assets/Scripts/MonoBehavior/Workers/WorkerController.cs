using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerController : MonoBehaviour,IListen
{
    public LanesDatabase lanes;
    public WorkerConfig wc;

    Rigidbody rb;
    bool turningRight = false;
    bool turningLeft = false;

    public GameState gamestate;

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
        gamestate.onPause.AddListener(UnRegisterListeners);
        gamestate.OnResume.AddListener(RegisterListeners);
        wc.onLeft.AddListener(MoveLeft);
        wc.onRight.AddListener(MoveRight);
    }

    public void OnDisable()
    {
        gamestate.onPause.RemoveListener(UnRegisterListeners);
        gamestate.OnResume.RemoveListener(RegisterListeners);
        wc.onLeft.RemoveListener(MoveLeft);
        wc.onRight.RemoveListener(MoveRight);
    }

    public void RegisterListeners()
    {
        wc.onLeft.AddListener(MoveLeft);
        wc.onRight.AddListener(MoveRight);
    }

    public void UnRegisterListeners()
    {
        wc.onLeft.RemoveListener(MoveLeft);
        wc.onRight.RemoveListener(MoveRight);
    }
}
