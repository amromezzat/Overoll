using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool instance;
    public Dictionary<string, PrefabCount> prefabsDict;

    Dictionary<string, Queue<GameObject>> poolDict;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		foreach(string pcName in prefabsDict.Keys)
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

    public GameObject GetFromPool(string instName)
    {
        if (poolDict.ContainsKey(instName))
        {
            Queue<GameObject> instQueue = poolDict[instName];
            if (instQueue.Count > 0)
            {
                GameObject pooledObj = instQueue.Dequeue();
                pooledObj.SetActive(true);
                return pooledObj;
            }
            GameObject newGameObj = Instantiate(prefabsDict[instName].prefab, prefabsDict[instName].parent.transform);
            return newGameObj;
        }
        Debug.LogError("Instance name is invalid");
        return null;
    }

    public void ReturnToPool(string instName, GameObject inst)
    {
        if (poolDict.ContainsKey(instName))
        {
            Queue<GameObject> instQueue = poolDict[instName];
            inst.SetActive(false);
            inst.transform.position = prefabsDict[instName].parent.transform.position;
            instQueue.Enqueue(inst);
        }
        Debug.LogError("Instance name is invalid");
    }
}
