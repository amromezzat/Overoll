using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class used to move the ground and act like the player keeps moving forward
/// </summary>

public class ObjectPooler : MonoBehaviour
{
    public List<ForTile> listOfPrefabs;
    public int poolSize;
    public List<GameObject> pool;
    /// <summary>
    /// Just to see in editor (malhosh lazma awi)
    /// </summary>
    public List<GameObject> activeElementsPool;
    public GameObject parent;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddToPool();
        }
    }

    public GameObject GetObjectFromPool()
    {
        {
            if (pool.Count == 0)
            {
                AddToPool();

            }

            GameObject element = pool[Random.Range(0, pool.Count)];
            element.SetActive(true);
            activeElementsPool.Add(element);
            pool.Remove(element);
            return element;
        }
    }

    public void ReturnObjectToPool(GameObject toreturn)
    {
        toreturn.transform.position = parent.transform.position;
        toreturn.SetActive(false);
        pool.Add(toreturn);
        activeElementsPool.Remove(toreturn);
    }

    void AddToPool()
    {
        var index = Random.Range(0, listOfPrefabs.Count);
        var obj = Instantiate<ForTile>(listOfPrefabs[index], parent.transform);
        obj.pool = this;
        obj.gameObject.SetActive(false);
        pool.Add(obj.gameObject);

    }

}