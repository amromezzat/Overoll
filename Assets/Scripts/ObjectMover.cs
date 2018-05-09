using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class resposiple for moving the tile
/// </summary>
public class ObjectMover : MonoBehaviour {
    public TileConfig tc;
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * -tc.tileSpeed;
    }
}

