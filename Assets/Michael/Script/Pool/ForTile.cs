using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTile : MonoBehaviour
{
    public ObjectPooler pool;
    private void Update()
    {
        if (gameObject.transform.position.z < -20)
        {
            pool.ReturnObjectToPool(this.gameObject);
        }
    }

}
