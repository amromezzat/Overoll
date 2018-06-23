using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvGenerator : MonoBehaviour, IHalt
{
    public Vector3 shift;

    public TileConfig tc;
    public GameData gameData;

    EnvPooler pool;
    Transform lastTile;

    bool isHalt;

    private void Awake()
    {
        RegisterListeners();
    }

    private void OnEnable()
    {
        isHalt = true;
    }

    void Start()
    {
        pool = gameObject.GetComponent<EnvPooler>();
        lastTile = transform;
        
    }

    private void Update()
    {
        if (!isHalt && pool.activeTileCount < 8)
        {
            GenerateTile();
        }
    }

    void GenerateTile()
    {
        var obj = pool.GetObjectFromPool();
        obj.transform.position = lastTile.transform.position + shift;
        lastTile = obj.transform;
    }


    public void RegisterListeners()
    {
        gameData.OnStart.AddListener(Begin);
        gameData.onPause.AddListener(Halt);
        gameData.OnResume.AddListener(Resume);
        gameData.onEnd.AddListener(End);
    }

    public void Begin()
    {
        isHalt = false;
    }

    public void Halt()
    {
        isHalt = true;
    }

    public void Resume()
    {
        isHalt = false;
    }

    public void End()
    {
        isHalt = true;
    }

    public bool drawGizmos = false;
    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Color(0, 1, 0, 0.5F);
            Gizmos.DrawCube(transform.position, new Vector3(10, 10, 1));
        }
    }
}
