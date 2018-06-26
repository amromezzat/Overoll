using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    ObjectReturner cReturn;
    public GameData gameData;

    void OnEnable()
    {
        cReturn = GetComponent<ObjectReturner>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Worker")
        {
            if (tag == "Magnet")
            {
                gameData.gotMagnet.Invoke();

            }
            if (tag == "Shield")
            {
                gameData.gotShield.Invoke();
            }
            StartCoroutine(cReturn.ReturnToPool(0));
        }
    }
}
