using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    public PoolDatabase pd;
    public int segmentActiveCount = 0;

    Dictionary<PoolableType, Queue<GameObject>> poolDict;

    private void Awake()
    {
        instance = this;
        poolDict = new Dictionary<PoolableType, Queue<GameObject>>(pd.poolableList.Count);
    }

    void Start()
    {
        foreach (PoolableObj po in pd.poolableList)
        {
            Queue<GameObject> instancesQueue = new Queue<GameObject>(po.count);
            for (int i = 0; i < po.count; i++)
            {
                GameObject newGameObj = InstantiateGameObj(po.prefab);
                instancesQueue.Enqueue(newGameObj);
            }
            poolDict[po.type] = instancesQueue;
        }
    }

    public GameObject GetFromPool(PoolableType instType)
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
            return InstantiateGameObj(pd[instType].prefab, true); ;
        }
        Debug.LogError("Instance is invalid", instType);
        return null;
    }

    public void ReturnToPool(PoolableType instType, GameObject inst)
    {
        if (!poolDict.ContainsKey(instType))
        {
            Debug.LogError("Instance is invalid", instType);
        }
        ObjectReturner instObjReturner = inst.GetComponent<ObjectReturner>();
        if (instObjReturner.inActiveSegment)
        {
            instObjReturner.inActiveSegment = false;
            segmentActiveCount--;
        }
        Queue<GameObject> instQueue = poolDict[instType];
        inst.SetActive(false);
        instQueue.Enqueue(inst);

    }

    public GameObject InstantiateGameObj(GameObject prefab, bool active = false)
    {
        GameObject newGameObj = Instantiate(prefab);
        newGameObj.SetActive(active);
        return newGameObj;
    }
}
