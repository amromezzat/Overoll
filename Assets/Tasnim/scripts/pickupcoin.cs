using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupcoin : MonoBehaviour {

    public int CoinCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        if (other.gameObject.name == "Player")
        {
            CoinCount += 1;
            Debug.Log(CoinCount);
        }
    }
}
