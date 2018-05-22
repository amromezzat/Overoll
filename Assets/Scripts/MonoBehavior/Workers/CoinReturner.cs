using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinReturner : MonoBehaviour {

    public ObjectPool objPool;
    public InteractablesDatabase iDB;
    public PoolableType poolablename;
    public GameState gState;
   public void CoinToPool()
    {
        poolablename = iDB[gameObject.name];
        objPool.ReturnToPool(poolablename, gameObject);
    }
    void Update()
    {

        if (gameObject.transform.position.z < gState.safeZone)
        {
            objPool.ReturnToPool(poolablename, gameObject);
        }
    }
}
