using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curency : MonoBehaviour
{
	public int gold;
	GameObject CurrencyUi;

	// Use this for initialization
	void Start () {
		CurrencyUi = GameObject.Find ("currency");

	}
	
	// Update is called once per frame
	void Update () {
		CurrencyUi.GetComponent<Text>().text = gold.ToString(); 

		if(gold < 0) //so that we don't have nof gold with -Ve 
		{
			gold = 0;

		}
	}
}