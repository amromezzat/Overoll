using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTile : MonoBehaviour {

    public ObjectPooler pool;
   
    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("Tile Manager").GetComponent<ObjectPooler>();

    }

    private void Update()
    {
        if (gameObject.transform.position.z  < pool.player.position.z - pool.safeZone)
        {
        pool.ReturnObjectToPool(this.gameObject);
        }
    }

}
