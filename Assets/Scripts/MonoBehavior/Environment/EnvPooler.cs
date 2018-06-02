using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move the environment around the player
/// </summary>

public class EnvPooler: MonoBehaviour
{
    public List<GameObject> listOfPrefabs;
    public int poolSize;
    //[HideInInspector]
    public List<GameObject> pool;
    public int activeTileCount = 0;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddToPool();
        }
    }

    public GameObject GetObjectFromPool()
    {
        activeTileCount++;
        {
            if (pool.Count == 0)
            {
                AddToPool();

            }

            GameObject element = pool[Random.Range(0, pool.Count)];
            element.SetActive(true);
            pool.Remove(element);
            return element;
        }
    }

    public void ReturnObjectToPool(GameObject returnedObj)
    {
        activeTileCount--;
        returnedObj.transform.position = gameObject.transform.position;
        returnedObj.SetActive(false);
        pool.Add(returnedObj);
    }

    void AddToPool()
    {
        int index = Random.Range(0, listOfPrefabs.Count);
        GameObject obj = Instantiate(listOfPrefabs[index], gameObject.transform);
        obj.SetActive(false);
        pool.Add(obj);

    }

}