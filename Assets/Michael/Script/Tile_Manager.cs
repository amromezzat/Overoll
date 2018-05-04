using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is depricated Now. Reference to class named ObjectPooler
/// </summary>

public class Tile_Manager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = -2.0f;
    private float tileLength = 2.0f;
    private int tilesToGenerate = 10;
    private float safeZone = 3.0f;
    private int lastPrfabIndex = 0;

    private List<GameObject> activeTiles;


    void Start()
    {
        activeTiles = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < tilesToGenerate; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
            SpawnTile();
        }
    }

    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - tilesToGenerate * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject generate;
        if (prefabIndex == -1)
            generate = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            generate = Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        generate.transform.SetParent(transform);
        generate.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(generate);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrfabIndex;
        while (randomIndex == lastPrfabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrfabIndex = randomIndex;
        return randomIndex;
    }
}
