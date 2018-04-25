using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 moveVector;
    public float speed = 5.0f;
   
   

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        moveVector = Vector3.zero;

        moveVector.x = Input.GetAxisRaw("Horizontal") * speed; //  for Left and Right movement

        moveVector.z = speed;                                     // for ongoing speed
        controller.Move(moveVector * Time.deltaTime);
    }
}
