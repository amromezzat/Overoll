using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerJump : MonoBehaviour,iHalt
{

    Rigidbody rb;
    Animator animator;
    float timeToJump = 0;
    bool jumping = false;
    bool readyToJump = true;
    float jumpT0;//jump start time

    public WorkerConfig wc;
    public TileConfig tc;

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
    void FixedUpdate()
    {
        if (jumping)
        {
            BreakJumping();
        }
    }

    //applying gravitational force to the body
    void BreakJumping()
    {
        rb.useGravity = true;
        Vector3 newVel = rb.velocity;
        newVel.y -= wc.gravityFactor * (Time.time - jumpT0);
        rb.velocity = newVel;
        // And test that the character is not on the ground again.
        //calculate platform height from equation platformHeigt(at x pos)
        if (transform.position.y < wc.groundLevel)
        {
            StopJumping();
        }
    }

    void StopJumping()
    {
        if (jumping)
        {
            animator.SetBool("JumpAnim", false);
            //set within platfrom height from equation platformHeigt(at x pos)
            Vector3 newPos = transform.position;
            newPos.y = wc.groundLevel;
            transform.position = newPos;
            Vector3 newVel = rb.velocity;
            newVel.y = 0;
            rb.velocity = newVel;
            jumping = false;
            readyToJump = true;
            rb.useGravity = false;
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
        animator.SetBool("JumpAnim", true);
        jumping = true;
        rb.AddForce(Vector3.up * wc.jumpSpeed, ForceMode.Impulse);
        jumpT0 = Time.time;
    }
     
    public void Halt()
    {
        wc.onJump.RemoveListener(Jump);
        wc.onSlide.RemoveListener(StopJumping);
    }

    public void Resume()
    {
        wc.onJump.AddListener(Jump);
        wc.onSlide.AddListener(StopJumping);
    }

    public void RegisterListeners()
    {
        gameData.OnStart.AddListener(Halt);
        gameData.onPause.AddListener(Halt);
        gameData.OnResume.AddListener(Resume);
    }
}
