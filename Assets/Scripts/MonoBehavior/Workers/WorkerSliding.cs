using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerSliding : MonoBehaviour,iHalt
{
    public WorkerConfig wc;
    public TileConfig tc;
    public GameData gameData;

    bool sliding = false;
    float slideTimer = 0f;
    public float maxSlideTime = 0.5f;
    float timeToSlide = 0;

    BoxCollider m_Collider;

    Animator animator;

    //-----------------------------------------------------------

    private void OnEnable()
    {
        RegisterListeners();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        m_Collider = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        if (sliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer > maxSlideTime)
            {
                StopSliding();
            }
        }
    }

    void StopSliding()
    {
        if (sliding)
        {
            sliding = false;
            animator.SetBool("DuckAnim", false);
            Vector3 newColliderSize = m_Collider.size;
            newColliderSize.y *= 2;
            m_Collider.size = newColliderSize;
            Vector3 colliderNewPos = m_Collider.transform.position;
            colliderNewPos.y *= 2;
            m_Collider.transform.position = colliderNewPos;
            Vector3 newPos = transform.position;
            newPos.y = wc.groundLevel;
            transform.position = newPos;
        }
    }

    //------------------------------------------------------------
    void Slide()
    {
        if (!sliding)
        {
            //-------------------------------------
            // for following workers to slide 
            timeToSlide = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
            StartCoroutine(SlideAfterDelay());
        }
    }
    //--------------------------------------------------
    // Coroutine for workers to slide

    IEnumerator SlideAfterDelay()
    {
        yield return new WaitForSeconds(timeToSlide);
        slideTimer = 0f;
        animator.SetBool("DuckAnim", true);

        // reduce  collider size by half during sliding ..
        Vector3 newColliderSize = m_Collider.size;
        newColliderSize.y *= 0.5f;
        m_Collider.size = newColliderSize;
        Vector3 newPosition = m_Collider.transform.position;
        newPosition.y *= 0.5f;
        m_Collider.transform.position = newPosition;
        sliding = true;
    }

    //---------------------------------------------
    //handling the states and listener to slide event 
  
    public void Halt()
    {
        wc.onSlide.RemoveListener(Slide);
        wc.onJump.RemoveListener(StopSliding);
    }

    public void Resume()
    {
        wc.onSlide.AddListener(Slide);
        wc.onJump.AddListener(StopSliding);
    }

    public void RegisterListeners()
    {
        gameData.OnStart.AddListener(Halt);
        gameData.onPause.AddListener(Halt);
        gameData.OnResume.AddListener(Resume);
    }
}
