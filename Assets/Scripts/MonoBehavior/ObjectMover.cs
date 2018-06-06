using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class resposiple for moving the tile
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour, iHalt
{
    public TileConfig tc;
    private Rigidbody rb;

    public GameData gameData;
    public Vector3 ExtraVelocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        RegisterListeners();
    }

    private void OnEnable()
    {
        if (gameData.gameState == GameState.Gameplay)
        {
            MoveObj();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void MoveObj()
    {
        rb.velocity = Vector3.back * tc.tileSpeed;
        if (transform.position.z > -3)
        {
            rb.velocity += ExtraVelocity;
        }
    }

    public void Halt()
    {
        rb.velocity = Vector3.zero;
    }

    public void Resume()
    {
        MoveObj();
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
        MoveObj();
    }

    public void End()
    {
        rb.velocity = Vector3.zero;
    }
}
