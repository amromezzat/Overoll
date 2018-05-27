using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerStrafe : MonoBehaviour
{
    public LanesDatabase lanes;
    public WorkerConfig wc;
    public float strafeTime = 0.5f;

    Rigidbody rb;
    Animator animator;

    bool strafing = false;
    float strafeTimer = 0;

    enum StrafeDirection
    {
        LEFT,
        RIGHT
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
            float completedPortion = strafeTimer / strafeTime;
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

    void Strafe(StrafeDirection direction)
    {
        if (!strafing)
        {
            strafeTimer = 0;

            switch (direction)
            {
                case StrafeDirection.RIGHT:
                    animator.SetBool("StrafeRightAnim", true);
                    lanes.GoRight();
                    break;
                case StrafeDirection.LEFT:
                    animator.SetBool("StrafeLeftAnim", true);
                    lanes.GoLeft();
                    break;
            }
            strafing = true;
        }
    }

    public void OnEnable()
    {
        wc.onLeft.AddListener(delegate { Strafe(StrafeDirection.LEFT); });
        wc.onRight.AddListener(delegate { Strafe(StrafeDirection.RIGHT); });
    }

    public void OnDisable()
    {
        wc.onLeft.RemoveListener(delegate { Strafe(StrafeDirection.LEFT); });
        wc.onRight.RemoveListener(delegate { Strafe(StrafeDirection.RIGHT); });
    }
}
