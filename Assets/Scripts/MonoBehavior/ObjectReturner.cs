using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturner : MonoBehaviour
{
    public TileConfig tc;
    public PoolableType poolableType;

    [HideInInspector]
    public bool inActiveSegment = false;

    public virtual IEnumerator ReturnToPool(float returnTime)
    {
        yield return new WaitForSeconds(returnTime);
        ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
    }
}
