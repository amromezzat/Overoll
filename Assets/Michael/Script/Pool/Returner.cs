using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Returner : MonoBehaviour
{
    public TileConfig tc;
    public ObjectPooler op;

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
