using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class used to move the ground and act like the player keeps moving forward
/// </summary>

public class EnvPooler: MonoBehaviour
{
    public List<Returner> listOfPrefabs;
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

    public void ReturnObjectToPool(GameObject toreturn)
    {
        activeTileCount--;
        toreturn.transform.position = gameObject.transform.position;
        toreturn.SetActive(false);
        pool.Add(toreturn);
    }

    void AddToPool()
    {
        var index = Random.Range(0, listOfPrefabs.Count);
        var obj = Instantiate<Returner>(listOfPrefabs[index], gameObject.transform);
        obj.gameObject.SetActive(false);
        pool.Add(obj.gameObject);

    }

}