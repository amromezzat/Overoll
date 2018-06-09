using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehaviour : MonoBehaviour, iHalt
{
    public LanesDatabase lanes;
    public GameData gd;
    public WorkerConfig wc;

    PositionWorker positionWorker;
    float strafeTimer = 0;
    Rigidbody rb;
    bool strafing = false;
    float newXPos = 0;
    IEnumerator randomCoroutine;

    void Awake()
    {
        positionWorker = GetComponent<PositionWorker>();
        rb = GetComponent<Rigidbody>();
        randomCoroutine = RandomWorker();
    }

    void OnEnable()
    {
        StartCoroutine(randomCoroutine);
    }

    void Update()
    {
        if (strafing)
        {
            strafeTimer += Time.deltaTime;
            float completedPortion = strafeTimer / wc.strafeDuration;
            float squarePortion = completedPortion * completedPortion;
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Lerp(transform.position.x, newXPos, squarePortion);
            transform.position = newPos;
            if (squarePortion >= 1)
            {
                strafeTimer = 0;
                strafing = false;
            }
        }
    }

    IEnumerator RandomWorker()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (strafing)
            {
                continue;
            }
            var r = Random.Range(0, 100);
            if (r < 80)
            {
                positionWorker.enabled = true;
            }
            else
            {
                strafing = true;
                positionWorker.enabled = false;
                newXPos = transform.position.x + Random.Range(-0.8f, 0.8f);
                rb.velocity = Vector3.zero;
            }
        }

    }

    public void Begin()
    {
        
    }

    public void Halt()
    {
        StopCoroutine(randomCoroutine);
    }

    public void Resume()
    {
        StartCoroutine(randomCoroutine);
    }

    public void End()
    {
        StopCoroutine(randomCoroutine);
    }

    public void RegisterListeners()
    {
        gd.OnStart.AddListener(Begin);
        gd.onPause.AddListener(Halt);
        gd.OnResume.AddListener(Resume);
        gd.onEnd.AddListener(End);
    }
}
