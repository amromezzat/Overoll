using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnetTrial : MonoBehaviour {

    public GameData gstate;
    TileReturner cReturn;
    Vector3 coinPos;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinPos = other.transform.position;
            

          //  FindObjectOfType<AudioManager>().PlaySound("Coin");

            StartCoroutine(cReturn.ReturnToPool(0));
            gstate.CoinCount += 1;
        }
    }
}
