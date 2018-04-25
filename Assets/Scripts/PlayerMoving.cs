using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 moveVector;
    public float speed = 5.0f;

    protected int currentPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentPosition = (int)transform.position.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);
        else if (Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);
        //moveVector = Vector3.zero;

        //moveVector.x = Input.GetAxisRaw("Horizontal") * speed; //  for Left and Right movement

        moveVector.z = speed;                                     // for ongoing speed
        controller.Move(moveVector * Time.deltaTime);
    }

    public void ChangeLane(int direction)
    {
        int targetPosition = currentPosition + direction;

        if (targetPosition < -1 || targetPosition > 1)
            return;

        currentPosition = targetPosition;
        transform.localPosition = new Vector3 (currentPosition, transform.localPosition.y , transform.localPosition.z);
    }
}
