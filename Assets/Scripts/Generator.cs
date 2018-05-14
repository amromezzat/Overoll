using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public Vector3 shift;
    public float disFromPlayer=10;
    EnvPooler pool;
    Transform lastTile;

    public TileConfig tc;

    /// <summary>
    /// t = d/v, d per prefab
    /// </summary>

	// Use this for initialization
	void Start () {

        pool = gameObject.GetComponent<ObjectPooler>();
        lastTile = this.transform;
        //StartCoroutine(GenerateCoroutine());
	}

    private void Update()
    {
        if(pool.activeTileCount < 8)
        {
            GenerateTile();
        }    
    }

    void GenerateTile()
    {
        var obj = pool.GetObjectFromPool();
        obj.transform.position = lastTile.transform.position + shift;
        lastTile = obj.transform;
    }

    //IEnumerator GenerateCoroutine()
    //{
    //    GenerateTile();
    //    yield return new WaitForSeconds(tc.spawnTime);
    //    StartCoroutine(GenerateCoroutine());
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(10, 10, 1));
    }


}
