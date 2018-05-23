using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public PoolDatabase pd;

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
                GameObject newGameObj = InstantiateGameObj(po.prefab, po.zOrigin);
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
            return InstantiateGameObj(pd[instType].prefab, pd[instType].zOrigin, true); ;
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
        Queue<GameObject> instQueue = poolDict[instType];
        inst.SetActive(false);
        Vector3 originPos = inst.transform.position;
        originPos.z = pd[instType].zOrigin;
        inst.transform.position = originPos;
        instQueue.Enqueue(inst);

    }

    public GameObject InstantiateGameObj(GameObject prefab, float zPos, bool active = false)
    {
        GameObject newGameObj = Instantiate(prefab);
        Vector3 originPos = newGameObj.transform.position;
        originPos.z = zPos;
        newGameObj.transform.position = originPos;
        newGameObj.SetActive(active);
        return newGameObj;
    }
}
