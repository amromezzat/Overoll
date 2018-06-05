using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehaviour : MonoBehaviour
{
    public LanesDatabase lanes;
    public float strafeDuration = 0.1f;

    PositionWorker pWactive;
    float strafeTimer = 0;
    Rigidbody rb;
    bool strafing = false;
    float newXPos = 0;

    void Awake()
    {

        pWactive = GetComponent<PositionWorker>();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {

        StartCoroutine(RandomWorker());
    }

    void Update()
    {
        if (strafing)
        {
            strafeTimer += Time.deltaTime;
            float completedPortion = strafeTimer / strafeDuration;
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
                yield return null;
            }
            var r = Random.Range(0, 100);
            if (r < 80)
            {
                pWactive.enabled = true;
            }
            else
            {
                strafing = true;
                pWactive.enabled = false;
                newXPos = transform.position.x + Random.Range(-0.8f, 0.8f);
                rb.velocity = Vector3.zero;

            }
        }

    }

}
