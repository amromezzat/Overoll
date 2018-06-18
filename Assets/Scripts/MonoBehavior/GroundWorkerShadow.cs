using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GroundWorkerShadow : MonoBehaviour {

    public WorkerConfig wc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = transform.position;
        newPos.y = wc.groundLevel + 0.03f;
        transform.position = newPos;
	}
}
