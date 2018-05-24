using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturner : MonoBehaviour {

    public InteractablesDatabase interactablesDB;
    public GameState gameState;
    public PoolableType poolableType;

    public void ReturnToObjectPool()
    {
        ObjectPool.instance.ReturnToPool(poolableType, gameObject);
    }

    void Update()
    {
        if (gameObject.transform.position.z < gameState.safeZone)
        {
            ObjectPool.instance.ReturnToPool(poolableType, gameObject);
        }
    }
}
