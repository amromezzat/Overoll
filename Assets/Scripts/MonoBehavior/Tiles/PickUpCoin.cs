using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpCoin : MonoBehaviour {

    public GameData gd;
    public WorkerConfig wc;
    TileReturner cReturn;
    private CoinMagnet coinMagnet;


    void Awake()
    {

        cReturn = GetComponent<TileReturner>();
        coinMagnet = GetComponent<CoinMagnet>();
        RegisterListeners();

    }

    public void OnEnable()
    {
        if (gd.magnetInAct)
        {
            coinMagnet.enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Worker" || other.tag == "SlaveMerger")
        {
            AudioManager.instance.PlaySound("Coin");

            StartCoroutine(cReturn.ReturnToPool(0));
            gd.CoinCount += 1;
        }
     }

    public void RegisterListeners()
    {
        gd.gotMagnet.AddListener(ActWithMagnet);
    }

    public void ActWithMagnet()
    {
            coinMagnet.enabled = true;
    }

    void OnDisable()
    {
        coinMagnet.enabled = false;
       
    }

}
