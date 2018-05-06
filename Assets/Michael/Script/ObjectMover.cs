using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour {
    public float multipler;
    public FloatValue speed;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        this.transform.position += (multipler * speed.value * Vector3.forward);
	}
}
