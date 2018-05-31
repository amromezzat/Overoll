using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturner : MonoBehaviour {

    public InteractablesDatabase interactablesDB;
    public GameData gameState;
    public PoolableType poolableType;

    public void ReturnToObjectPool()
    {
        ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
    }

    void Update()
    {
        if (gameObject.transform.position.z < gameState.safeZone)
        {
            ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
        }
    }
}
