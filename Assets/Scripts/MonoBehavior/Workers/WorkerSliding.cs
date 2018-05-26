using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerSliding : MonoBehaviour
{

    Rigidbody rb;
    bool sliding = false;
    float slideTimer = 0f;
    public float maxSlideTime = 0.5f;

    float timeToSlide = 0;

    public WorkerConfig wc;
    public TileConfig tc;

    BoxCollider m_Collider;
    float m_ScaleX, m_ScaleY, m_ScaleZ;

    public GameState gamestat;

    Animator animator;

    //-----------------------------------------------------------
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        m_Collider = GetComponent<BoxCollider>();

        m_ScaleX = 1.0f;
        m_ScaleY = 1.0f;
        m_ScaleZ = 1.0f;

    }

    private void FixedUpdate()
    {
        if (sliding)
        {
            Debug.Log("sliding");
            slideTimer += Time.deltaTime;
            if (slideTimer > maxSlideTime)
            {
                sliding = false;
                animator.SetBool("isSliding", false);

                // return the collider size to its original 
                m_ScaleX = 2.0f;
                m_ScaleY = 2.0f;
                m_ScaleZ = 2.0f;
                m_Collider.size = new Vector3(m_Collider.size.x * m_ScaleX, m_Collider.size.y * m_ScaleY, m_Collider.size.z * m_ScaleZ);
            }
        }
    }

    void SlidingAction()
    {
        slideTimer = 0f;
        animator.SetBool("isSliding", true);

        // reduce  collider size by half during sliding ..

        m_ScaleX = 0.5f;
        m_ScaleY = 0.5f;
        m_ScaleZ = 0.5f;
        m_Collider.size = new Vector3(m_Collider.size.x * m_ScaleX, m_Collider.size.y * m_ScaleY, m_Collider.size.z * m_ScaleZ);

        sliding = true;
    }
    //------------------------------------------------------------
    void Slide()
    {
        if (!sliding)
        {
            SlidingAction();
            //-------------------------------------
            // for following workers to slide 
            timeToSlide = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
            StartCoroutine(slideAfterDelay());

        }
    }
    //--------------------------------------------------
    // Coroutine for workers to slide

    IEnumerator slideAfterDelay()
    {
        yield return new WaitForSeconds(timeToSlide);
        SlidingAction();

    }

    //---------------------------------------------
    //handling the states and listener to slide event 
    public void OnEnable()
    {
        gamestat.onPause.AddListener(UnRegisterListeners);
        gamestat.OnResume.AddListener(RegisterListeners);
        wc.onSlide.AddListener(Slide);
    }

    public void OnDisable()
    {
        gamestat.onPause.RemoveListener(UnRegisterListeners);
        gamestat.OnResume.RemoveListener(RegisterListeners);
        wc.onSlide.RemoveListener(Slide);
    }

    public void RegisterListeners()
    {
        wc.onSlide.AddListener(Slide);
    }

    public void UnRegisterListeners()
    {
        wc.onSlide.RemoveListener(Slide);
    }

}
