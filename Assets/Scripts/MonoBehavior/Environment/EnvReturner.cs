using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class returns the tile back to the pool
/// </summary>
[RequireComponent(typeof(EnvPooler))]
public class EnvReturner : MonoBehaviour
{
    public TileConfig tc;
    private EnvPooler op;

    private void Awake()
    {
        op = GetComponentInParent<EnvPooler>();
    }

    private void Update()
    {
        if (gameObject.transform.position.z < tc.disableSafeDistance)
        {
            op.ReturnObjectToPool(gameObject);
        }
    }

}
