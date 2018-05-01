using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObsTest : MonoBehaviour {

    public int speed = 5;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity += Vector3.back * speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z < -20)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 20);
        }
	}
}
