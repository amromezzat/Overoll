using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpCoin : MonoBehaviour {

    public GameData gstate;
    TileReturner cReturn;

    void OnEnable()
    {
        cReturn=GetComponent<TileReturner>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Worker")
        {
            FindObjectOfType<AudioManager>().PlaySound("Coin");

            cReturn.ReturnToPool();
            gstate.CoinCount += 1;
        }
     }
}
