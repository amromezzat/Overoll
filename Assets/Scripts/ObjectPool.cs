using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool instance;
    public Dictionary<TileType, PrefabCount> prefabsDict;

    Dictionary<TileType, Queue<GameObject>> poolDict;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		foreach(TileType pcName in prefabsDict.Keys)
        {
            Queue < GameObject > instancesQueue = new Queue<GameObject>(prefabsDict[pcName].count);
            for(int i = 0; i < prefabsDict[pcName].count; i++)
            {
                GameObject newGameObj = Instantiate(prefabsDict[pcName].prefab, prefabsDict[pcName].parent.transform);
                newGameObj.SetActive(false);
                instancesQueue.Enqueue(newGameObj);
            }
            poolDict[pcName] = instancesQueue;
        }
	}

    public GameObject GetFromPool(TileType instType)
    {
        if (poolDict.ContainsKey(instType))
        {
            Queue<GameObject> instQueue = poolDict[instType];
            if (instQueue.Count > 0)
            {
                GameObject pooledObj = instQueue.Dequeue();
                pooledObj.SetActive(true);
                return pooledObj;
            }
            GameObject newGameObj = Instantiate(prefabsDict[instType].prefab, prefabsDict[instType].parent.transform);
            return newGameObj;
        }
        Debug.LogError("Instance is invalid");
        return null;
    }

    public void ReturnToPool(TileType instType, GameObject inst)
    {
        if (poolDict.ContainsKey(instType))
        {
            Queue<GameObject> instQueue = poolDict[instType];
            inst.SetActive(false);
            inst.transform.position = prefabsDict[instType].parent.transform.position;
            instQueue.Enqueue(inst);
        }
        Debug.LogError("Instance is invalid");
    }
}
