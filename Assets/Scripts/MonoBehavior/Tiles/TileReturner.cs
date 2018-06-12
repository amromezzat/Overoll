using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileReturner : ObjectReturner {

    void Update()
    {
        if (gameObject.transform.position.z < tc.disableSafeDistance)
        {
            ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
        }
    }
}
