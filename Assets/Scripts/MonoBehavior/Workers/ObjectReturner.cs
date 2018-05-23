using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturner : MonoBehaviour {

    public InteractablesDatabase iDB;
    public GameState gState;
    public PoolableType poolableType;

    public void ReturnToObjectPool()
    {
        ObjectPool.instance.ReturnToPool(poolableType, gameObject);
    }

    void Update()
    {

        if (gameObject.transform.position.z < gState.safeZone)
        {
            ObjectPool.instance.ReturnToPool(poolableType, gameObject);
        }
    }
}
