using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : IDoAction
{
    GameObject player;

    GameObject[] sceneCoins;

    float collectTimer;
    float collectDuration=0.2f;
   
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Worker");
    }

    GameObject[] GetCoinsInTheScene()
    {
        sceneCoins = GameObject.FindGameObjectsWithTag("coin");
      
        return sceneCoins;
    }

    public void OnStateEnter(Animator animator)
    {
        GetCoinsInTheScene();
    }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        for (int i = 0; i < sceneCoins.Length; i++)
        {
            Vector3 coinPos = new Vector3(sceneCoins[i].transform.position.x, sceneCoins[i].transform.position.y, sceneCoins[i].transform.position.z);
            Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            collectTimer += Time.deltaTime;
            float completedPortion = collectTimer / collectDuration;
            coinPos.x = Mathf.Lerp(coinPos.x, playerPos.x, Mathf.Sin(Mathf.PI * completedPortion));
            coinPos.y = Mathf.Lerp(coinPos.y, playerPos.y, Mathf.Sin(Mathf.PI * completedPortion));
            coinPos.z = Mathf.Lerp(coinPos.z, playerPos.z, Mathf.Sin(Mathf.PI * completedPortion));
            
            transform.position = coinPos;

            
        }

        return true; 
    }

    public void OnStateExit(Animator animator)
    {
        throw new NotImplementedException();
    }
}
