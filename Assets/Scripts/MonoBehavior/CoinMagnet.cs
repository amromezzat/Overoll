using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    TileReturner tileReturner;

    Vector3 coinPos;
    Transform playerTrans;
    float lerpFac;
    float totalTime = 0.5f;
    float currentTimer = 0;
    bool collided = false;
    float timerCoolDown;
    float yPos;

    const float cdBeforeCollision = 0.3f;

    void Awake()
    {
        tileReturner = GetComponent<TileReturner>();
        yPos = transform.position.y;
        timerCoolDown = cdBeforeCollision;
    }

    void OnEnable()
    {
        collided = false;
        currentTimer = 0;
        timerCoolDown = cdBeforeCollision;
        Vector3 fixedYPos = transform.position;
        fixedYPos.y = yPos;
        transform.position = fixedYPos;
    }

    void Update()
    {
        if (collided)
        {
            currentTimer += Time.deltaTime;
            lerpFac = currentTimer / totalTime;
            transform.position = Vector3.Lerp(coinPos, playerTrans.position, lerpFac);
            if(lerpFac > 1)
            {
                StartCoroutine(tileReturner.ReturnToPool(0));
            }
        }
        else
        {
            timerCoolDown -= Time.deltaTime;
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
            collided = true;
            playerTrans = other.transform.parent;
            coinPos = transform.position;
        }
    }
}
