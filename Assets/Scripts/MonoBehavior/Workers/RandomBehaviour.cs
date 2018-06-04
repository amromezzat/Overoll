using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehaviour : MonoBehaviour {
    public LanesDatabase lanes;
    PositionWorker pWactive;
    public float strafeDuration = 0.1f;
    float strafeTimer = 0;
    public float exponentialPortion;
    Rigidbody rb;

    void Awake()
    {
        
        pWactive =GetComponent<PositionWorker>();
        rb = GetComponent<Rigidbody>();
    }

   void OnEnable()
    {
        
        StartCoroutine(randomworker());
    }

    void FixedUpdate()
    {
        float completedPortion = strafeTimer / strafeDuration;
         exponentialPortion = completedPortion * completedPortion;
       // changeposition();
    }

    IEnumerator randomworker()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            var r = Random.Range(0, 100);
            if (r < 80)
            {
                pWactive.enabled = true;
            }

            else
            {
                pWactive.enabled = false;
                //float xPos = Mathf.Lerp(transform.position.x, lanes[(int)Random.Range(0, 4)].laneCenter, exponentialPortion);
                //strafeTimer += Time.deltaTime;
                //transform.position = new Vector3(lanes[(int)Random.Range(0, 4)].laneCenter, transform.position.y, transform.position.z);
                //rb.velocity = new Vector3(0, 0, 0);
                //Debug.Log("sss");
                //pWactive.enabled = true;

                transform.position = new Vector3((transform.position.x + Random.Range(-0.8f, 0.8f)), transform.position.y, transform.position.z);

                rb.velocity = new Vector3(0, 0, 0);
                

            }
        }

    }

    void changeposition()
    {
        
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3((transform.position.x + Random.Range(-0.5f, 0.5f)), transform.position.y, transform.position.z);
        
    }

}
