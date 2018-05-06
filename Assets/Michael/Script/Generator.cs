using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public Vector3 shift;
    ObjectPooler pool;
    Transform lastTile;

	// Use this for initialization
	void Start () {
        pool = this.GetComponent<ObjectPooler>();
        lastTile = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Generate();
	}

    void Generate()
    {
        var obj= pool.GetObjectFromPool();
        obj.transform.position = lastTile.transform.position + shift;
        lastTile = obj.transform;
    }
    
}
