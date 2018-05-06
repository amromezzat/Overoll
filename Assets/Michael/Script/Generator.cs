using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public Vector3 shift;
    public float disFromPlayer=10;
    ObjectPooler pool;
    Transform lastTile;

    public TileConfig tc;

    public float time = 0.2f;

	// Use this for initialization
	void Start () {

        pool = gameObject.GetComponent<ObjectPooler>();
        lastTile = this.transform;
        StartCoroutine(Generate());
	}

    private void FixedUpdate()
    {
        
    }

    IEnumerator Generate()
    {
        var obj= pool.GetObjectFromPool();
        obj.transform.position = lastTile.transform.position + shift;
        lastTile = obj.transform;
        yield return new WaitForSeconds(time);
        StartCoroutine(Generate());
    }

    
    
}
