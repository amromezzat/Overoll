﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturner : MonoBehaviour
{
    public InteractablesDatabase interactablesDB;
    public TileConfig tileConfig;
    public PoolableType poolableType;

    [HideInInspector]
    public bool inActiveSegment = false;

    public void ReturnToObjectPool()
    {
        ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
    }
}
