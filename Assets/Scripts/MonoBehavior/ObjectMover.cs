using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class resposiple for moving the tile
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour,iHalt
{
    public TileConfig tc;
    private Rigidbody rb;
 

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();  
    }

    public void Halt()
    {
        rb.velocity = Vector3.zero;
    }

    public void Resume()
    {
        rb.velocity += Vector3.forward * -tc.tileSpeed;
    }
}

