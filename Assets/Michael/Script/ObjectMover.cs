using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour {
    public TileConfig tc;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = Vector3.forward * tc.tileSpeed;
	}
	// Update is called once per frame
	void Update () {
        //this.transform.position += (tc.tileSpeed * Vector3.forward);
	}
}
