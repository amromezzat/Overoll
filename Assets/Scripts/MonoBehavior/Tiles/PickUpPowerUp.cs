using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour {

 
    TileReturner cReturn;
    GameObject gameobj;
    public WorkerConfig wc;

    void OnEnable()
    {
      
        cReturn = GetComponent<TileReturner>();
        gameobj = GetComponent<GameObject>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Worker")
        {
            if (gameobj.tag == "Magnet")
            {
                wc.gotMagnet.Invoke();  
            }
            if(gameobj.tag=="Shield")
            {
                wc.gotShield.Invoke();
            }
           cReturn.ReturnToObjectPool();
           
        }
    }
}
