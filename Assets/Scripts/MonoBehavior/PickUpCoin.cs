using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour {

    public GameState gstate;
    public CoinReturner cReturn;
  
     void OnEnable()
    {
        cReturn=GetComponent<CoinReturner>();
    }
    public void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "worker")
        {
            cReturn.CoinToPool();
            gstate.CoinCount += 1;
            Debug.Log(gstate.CoinCount);
        }
     }
}
