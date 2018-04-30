using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public WorkerConfig wc;
    public int jumpSpeed = 10;
    public float gravityFactor = 10;
    int laneNum = 2;
    Rigidbody rb;
    bool jumping = false;
    float t0;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //only jump if it is not yet currently jumping
            if (!jumping)
            {
                rb.velocity += new Vector3(0, jumpSpeed);
                jumping = true;
                t0 = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(laneNum-1<laneNum/2)
                return;
            Vector3 newPos = gameObject.transform.position;
            newPos.x -= wc.laneWidth;
            gameObject.transform.position = newPos;
        }
    }

    void FixedUpdate()
    {
        if (jumping)
        {
            newVel = rb.velocity;
            newVel.y -= gravityFactor * (Time.time - t0);
            rb.velocity = newVel;

            // And test that the character is not on the ground again.
            //calculate platform height from equation platformHeigt(at x pos)
            if (transform.position.y < 0)
            {
                //set within platfrom height from equation platformHeigt(at x pos)
                newPos = transform.position;
                newPos.y = 0;
                transform.position = newPos;
                newVel.y = 0;
                rb.velocity = newVel;
                jumping = false;
            }
        }
    }
}
