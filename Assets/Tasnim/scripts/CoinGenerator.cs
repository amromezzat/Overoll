using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    CoinPool coinPoolScript;
    public Vector3 disBetweenCoins;
    public Lanes lanes;
    public int numberFollowedCoins;
    public EnvGenerator gen;
    public GameObject player;
    public float playerPosY = 0.75f;
    public float timeBetweenFollowedCoins = 1.0f;
    public float shift;

    void Start()
    {
        coinPoolScript = this.GetComponent<CoinPool>();
        StartCoroutine(Generate());


    }

    IEnumerator Generate()
    {
        int laneNumber = Random.Range(0, 5);
        for (int i = 0; i < 10; i++)
        {
            GameObject generatedcoin = coinPoolScript.GetCoinFromPool();
            //call get coin from the coin pool
            Vector3 pos = generatedcoin.transform.position;


            pos.x = lanes[laneNumber].laneCenter;

            shift = i * 2;
            generatedcoin.transform.position = new Vector3(pos.x, playerPosY, gen.transform.position.z + shift);
            yield return null;
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(Generate());
    }
}
