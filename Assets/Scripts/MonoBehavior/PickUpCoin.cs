using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpCoin : MonoBehaviour {

    public GameState gstate;
    ObjectReturner cReturn;

    void OnEnable()
    {
        cReturn=GetComponent<ObjectReturner>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Worker")
        {
            cReturn.ReturnToObjectPool();
            gstate.CoinCount += 1;
          //  Debug.Log(gstate.CoinCount);
        }
     }
}
