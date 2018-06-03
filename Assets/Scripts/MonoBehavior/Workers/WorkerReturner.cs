using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerReturner : MonoBehaviour {

    public InteractablesDatabase interactablesDB;
    public TileConfig tileConfig;
    public PoolableType poolableType;

    public void ReturnToObjectPool()
    {
        ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
    }
}
