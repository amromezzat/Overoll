using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerReturner : ObjectReturner
{
    public WorkerConfig wc;
    bool dying = false;

    private void Update()
    {
        if (dying)
        {
            Vector3 groundPos = transform.position;
            groundPos.y = Mathf.Lerp(groundPos.y, wc.groundLevel, 6 * Time.deltaTime);
            transform.position = groundPos;
            if (transform.position.y <= wc.groundLevel)
            {
                dying = false;
            }
        }
    }

    public override IEnumerator ReturnToPool(float returnTime)
    {
        dying = true;
        yield return new WaitForSeconds(returnTime);
        dying = false;
        ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
    }
}
