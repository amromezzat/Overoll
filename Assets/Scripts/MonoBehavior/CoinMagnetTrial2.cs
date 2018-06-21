using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnetTrial2 : MonoBehaviour
{
    
    Vector3 coinPos;
    Transform playerTrans;
    float lerpFac;
    float totalTime = 0.5f;
    float currentTimer = 0;
    bool collided = false;
    float timerCoolDown=0.5f;
    float yPos;

    void Awake()
    {
        yPos = transform.position.y;
    }

    void Update()
    {
        timerCoolDown -= Time.deltaTime;
        //Debug.Log(collided);

        if (collided)
        {
            currentTimer += Time.deltaTime;
            lerpFac = currentTimer / totalTime;
            transform.position = Vector3.Lerp(coinPos, playerTrans.position, lerpFac);
        }
    }
    void OnTriggerEnter(Collider other)
    {
      
        if (timerCoolDown > 0)
        {
            
            return;
            
        }
        if (other.gameObject.CompareTag("PowerUpCollider"))
        {
         //   print("coin with magnet");
            collided = true;
            playerTrans = other.transform.parent;
            coinPos = transform.position;
           
        }


    }
    void OnDisable()
    {
        collided = false;
        currentTimer = 0;
        timerCoolDown = 0.5f;
        transform.position = new Vector3(0, yPos, 0);
    }

   
}
