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
        if (other.tag == "Worker" || other.tag == "SlaveMerger")
        {
            FindObjectOfType<AudioManager>().PlaySound("Coin");

            StartCoroutine(cReturn.ReturnToPool(0));
            gstate.CoinCount += 1;
        }
     }
}
