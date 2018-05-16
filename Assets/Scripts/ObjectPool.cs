using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public PoolableDatabase pd;

    Dictionary<TileType, Queue<GameObject>> poolDict;

    private void Awake()
    {
        instance = this;
        poolDict = new Dictionary<TileType, Queue<GameObject>>(pd.poolableList.Count);
    }

    // Use this for initialization
    void Start()
    {
        foreach (PoolableObj po in pd.poolableList)
        {
            Queue<GameObject> instancesQueue = new Queue<GameObject>(po.count);
            for (int i = 0; i < po.count; i++)
            {
                GameObject newGameObj;
                if (po.parent)
                    newGameObj = Instantiate(po.prefab, po.parent.transform);
                else
                    newGameObj = Instantiate(po.prefab);
                newGameObj.SetActive(false);
                instancesQueue.Enqueue(newGameObj);
            }
            poolDict[po.type] = instancesQueue;
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
            GameObject newGameObj = Instantiate(pd[instType].prefab, pd[instType].parent.transform);
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
            inst.transform.position = pd[instType].parent.transform.position;
            instQueue.Enqueue(inst);
        }
        Debug.LogError("Instance is invalid");
    }
}
