using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {
    coinbool coinpoolscript;
    public Vector3 disbetweencoins;

    public int numberfollowedcoins;

    void Start()
    {
        coinpoolscript = this.GetComponent<coinbool>();

    }


    void Generate()
    {
        for (int i = 0; i < numberfollowedcoins; i++)
        {
            GameObject generatedcoin = coinpoolscript.GetCoinFromPool();
            //call get coin from the coin pool

            generatedcoin.transform.position
        }
        
    }
}
