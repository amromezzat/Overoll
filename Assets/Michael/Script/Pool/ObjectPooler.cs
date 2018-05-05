using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class used to move the ground and act like the player keeps moving forward
/// </summary>

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> listOfPrefabs;
    public List<GameObject> pool;
    public List<GameObject> activeElementsPool;

    public GameObject parent;
    private GameObject lastGen;

    public Transform player;

    public float safeZone;

    public float tileSize = 2.0f;
    float errorMargin = 0.0f;
    float distanceBtTiles = 0.0f;

    public float speed;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        for (int i = 0; i < 10; i++)
        {
            int index = 0;
            if (i > 4)
            {
                index = i % 4;
            }
            GameObject obj = Instantiate(listOfPrefabs[index], parent.transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private void Start()
    {
        GameObject el = GetObjectFromPool();
        MoveTile(el);
        lastGen = el;
    }

    public void FixedUpdate()
    {
        distanceBtTiles = parent.transform.position.z - lastGen.transform.position.z;
        if (distanceBtTiles >= tileSize)
        {
            errorMargin = distanceBtTiles - tileSize;
            distanceBtTiles -= errorMargin;
        }

        if (distanceBtTiles == tileSize)
        {
            GameObject el = GetObjectFromPool();
            MoveTile(el);
            lastGen = el;
        }
    }

    public GameObject GetObjectFromPool()
    {
        {
            if (pool.Count == 0)
            {
                GameObject obj = Instantiate(listOfPrefabs[0], parent.transform);
                obj.SetActive(false);
                pool.Add(obj);
                return null;
            }
            else
            {
                GameObject element = pool[Random.Range(0, pool.Count)];
                element.SetActive(true);
                activeElementsPool.Add(element);
                pool.Remove(element);
                lastGen = element;
                return element;
            }
        }
    }

    public void ReturnObjectToPool(GameObject toreturn)
    {
        toreturn.transform.position = parent.transform.position;
        toreturn.SetActive(false);
        pool.Add(toreturn);
        activeElementsPool.Remove(toreturn);
    }

    public void MoveTile(GameObject element)
    {
        element.GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
    }
}