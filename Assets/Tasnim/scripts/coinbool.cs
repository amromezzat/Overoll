using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinbool : MonoBehaviour {

    public GameObject prefab;
    public int coinpoolSize;
    public List<GameObject> CoinPool;

    // we need random position to start in random lanes 

	// Use this for initialization
	void Start () {

        for (int i = 0; i < coinpoolSize; i++)
        {
            AddToCoinPool();
        }
		
	}
    //------------------------------------------------
    public GameObject GetCoinFromPool()
    {
        if (CoinPool.Count == 0)
        {
            AddToCoinPool();
        }
        GameObject Element = CoinPool[1];
        Element.SetActive(true);
       
        CoinPool.Remove(Element);
        return Element;
    } 
    //----------------------------------------------
    public void ReturnCoinToPool(GameObject ObjToReturn)
    {
        ObjToReturn.SetActive(false);
        CoinPool.Add(ObjToReturn);
    }
    //----------------------------------------------
    void AddToCoinPool()
    {
        var obj = Instantiate(prefab);
        obj.SetActive(false);
        CoinPool.Add(obj);

    }
	
	
}
