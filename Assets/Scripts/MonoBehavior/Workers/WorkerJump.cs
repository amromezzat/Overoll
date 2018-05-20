using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerJump : MonoBehaviour {

    Vector2 newVelocity;
    Rigidbody rb;
    float timeToJump = 0;
    bool jumping = false;
    bool readyToJump = true;
    float jumpt0;//jump start time
    public WorkerConfig wc;
    public TileConfig tc;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        StopJumping();
    }
    //applying gravitational force to the body
    void StopJumping()
    {
        if (jumping)
        {
            Vector3 newVel = rb.velocity;
            newVel.y -= wc.gravityFactor * (Time.time - jumpt0);
            rb.velocity = newVel;
            // And test that the character is not on the ground again.
            //calculate platform height from equation platformHeigt(at x pos)
            if (transform.position.y < wc.groundLevel)
            {
                //set within platfrom height from equation platformHeigt(at x pos)
                Vector3 newPos = transform.position;
                newPos.y = wc.groundLevel;
                transform.position = newPos;
                newVel.y = 0;
                rb.velocity = newVel;
                jumping = false;
                readyToJump = true;
            }
        }
    }

    private void Jump()
    {
        if (readyToJump)
        {
            timeToJump = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
            StartCoroutine(JumpAfterDelay());
        }
    }

    IEnumerator JumpAfterDelay()
    {
        readyToJump = false;
        yield return new WaitForSeconds(timeToJump);
        jumping = true;
        rb.velocity += Vector3.up * wc.jumpSpeed;
        jumpt0 = Time.time;
    }

    public void OnEnable()
    {
        wc.onJump.AddListener(Jump);
    }

    public void OnDisable()
    {
        wc.onJump.RemoveListener(Jump);
    }
}
