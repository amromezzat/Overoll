using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMoney : MonoBehaviour {

	Curency currencyscript;
	 
	public int addmoney;

	// Use this for initialization
	void Start () 
	{
		currencyscript = GameObject.FindWithTag ("GameController").GetComponent<Curency> ();
		
	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.gameObject.tag == "Worker") 
		{
			currencyscript.gold += addmoney;
			Destroy (gameObject);
		}
	}
	

}
