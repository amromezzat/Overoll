using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour {

    public GameObject prefab;
    public int coinPoolSize;
    public List<GameObject> coinPool;
    public GameObject coinParent;

    // we need random position to start in random lanes 
    
	// Use this for initialization
	void Start () {

        for (int i = 0; i < coinPoolSize; i++)
        {
            AddToCoinPool();
        }
		
	}
    //------------------------------------------------
    public GameObject GetCoinFromPool()
    {
        if (coinPool.Count == 0)
        {
            AddToCoinPool();
        }
        GameObject Element = coinPool[0];
        Element.SetActive(true);
        Debug.Log("coin did active");
        coinPool.Remove(Element);
        return Element;
    } 
    //----------------------------------------------
    public void ReturnCoinToPool(GameObject ObjToReturn)
    {
        ObjToReturn.SetActive(false);
        coinPool.Add(ObjToReturn);
    }
    //----------------------------------------------
    void AddToCoinPool()
    {
        var obj = Instantiate(prefab,coinParent.transform);
        obj.SetActive(false);
        coinPool.Add(obj);

    }
}
