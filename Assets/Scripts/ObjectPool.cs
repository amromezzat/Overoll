using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public PoolableDatabase pd;

    Dictionary<EnumValue, Queue<GameObject>> poolDict;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        foreach (EnumValue pcType in pd.Keys)
        {
            Queue<GameObject> instancesQueue = new Queue<GameObject>(pd[pcType].count);
            for (int i = 0; i < pd[pcType].count; i++)
            {
                GameObject newGameObj = Instantiate(pd[pcType].prefab, pd[pcType].parent.transform);
                newGameObj.SetActive(false);
                instancesQueue.Enqueue(newGameObj);
            }
            poolDict[pcType] = instancesQueue;
        }
    }

    public GameObject GetFromPool(EnumValue instType)
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

    public void ReturnToPool(EnumValue instType, GameObject inst)
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
