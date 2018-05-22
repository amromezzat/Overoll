using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour {

    public GameState gstate;
    CoinReturner cReturn;
  
     void OnEnable()
    {
        cReturn=GetComponent<CoinReturner>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Worker")
        {
            cReturn.CoinToPool();
            gstate.CoinCount += 1;
            Debug.Log(gstate.CoinCount);
        }
     }
}
