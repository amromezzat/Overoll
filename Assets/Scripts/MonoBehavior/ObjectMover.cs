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

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        if (gameData.gameState == GameState.startState || gameData.gameState == GameState.gamePlayState)
        {
            rb.velocity = Vector3.forward * -tc.tileSpeed;
        }
        RegisterListeners();
    }

    public void Halt()
    {
        rb.velocity = Vector3.zero;
    }

    public void Resume()
    {
        rb.velocity = Vector3.forward * -tc.tileSpeed + ExtraVelocity;
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
        rb.velocity = Vector3.forward * -tc.tileSpeed;
    }

    public void End()
    {
        rb.velocity = Vector3.zero;
    }
}
