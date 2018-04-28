using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCam : MonoBehaviour
{

    private Transform playerToLookAt;
    private Vector3 offset;
    private Vector3 moveVector;
    void Start()
    {
        playerToLookAt = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerToLookAt.position;
    }

    void Update()
    {
        moveVector = playerToLookAt.position + offset;
        moveVector.x = 0;                                   // to limit the camera following x movement 

        transform.position = moveVector;
    }
}
