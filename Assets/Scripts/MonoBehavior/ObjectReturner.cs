using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturner : MonoBehaviour {

    public InteractablesDatabase interactablesDB;
    public GameData gameData;
    public PoolableType poolableType;

    public void ReturnToObjectPool()
    {
        ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
    }

    void Update()
    {
        if (gameObject.transform.position.z < gameData.safeZone)
        {
            ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
        }
    }
}
