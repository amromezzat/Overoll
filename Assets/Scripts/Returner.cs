﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class returns the tile back to the pool
/// </summary>
public class Returner : MonoBehaviour
{
    public TileConfig tc;
    private ObjectPooler op;

    private void Start()
    {
        op = GetComponentInParent<ObjectPooler>();
    }

    private void Update()
    {
        if (gameObject.transform.position.z < -5)
        {
            op.ReturnObjectToPool(this.gameObject);
        }
    }

}