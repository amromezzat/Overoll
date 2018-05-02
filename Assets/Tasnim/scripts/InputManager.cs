using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{

    public float maxTime;
    public float minSwipDis;

    float startTime;
    float endTime;

    Vector3 startPos;
    Vector3 endpos;

    float swipdis;
    float swiptime;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(PrintPlatform());


    }

    IEnumerator PrintPlatform()
    {
        print(Application.platform);
        yield return new WaitForSeconds(1);
        StartCoroutine(PrintPlatform());
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hu");

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startTime = Time.time;
                    startPos = touch.position;

                    //   Debug.Log("start touch");

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTime = Time.time;
                    endpos = touch.position;
                    //      Debug.Log("End touch");

                    swipdis = (endpos - startPos).magnitude;
                    swiptime = (endTime - startTime);

                    if (swiptime < maxTime && swipdis > minSwipDis)
                    {
                        //  Debug.Log("enter the conditions ");
                        Swip();
                    }

                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            { Debug.Log("up arrow"); }
            else if (Input.GetKey(KeyCode.S))
            { Debug.Log("down arrow"); }
            else if (Input.GetKey(KeyCode.D))
            { Debug.Log("right arrow"); }
            else if (Input.GetKey(KeyCode.A))
            { Debug.Log("left arrow"); }
            else if (Input.GetKey(KeyCode.Space))
            { Debug.Log("space"); }

        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetKeyDown(KeyCode.W))
            { Debug.Log("up arrow"); }
            else if (Input.GetKey(KeyCode.S))
            { Debug.Log("down arrow"); }
            else if (Input.GetKey(KeyCode.D))
            { Debug.Log("right arrow"); }
            else if (Input.GetKey(KeyCode.A))
            { Debug.Log("left arrow"); }
            else if (Input.GetKey(KeyCode.Space))
            { Debug.Log("space"); }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startTime = Time.time;
                    startPos = touch.position;

                    //   Debug.Log("start touch");

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTime = Time.time;
                    endpos = touch.position;
                    //      Debug.Log("End touch");

                    swipdis = (endpos - startPos).magnitude;
                    swiptime = (endTime - startTime);

                    if (swiptime < maxTime && swipdis > minSwipDis)
                    {
                        //  Debug.Log("enter the conditions ");
                        Swip();
                    }

                }
            }


        }
        //Debug.Log("ANDROID Editor");
    }


    void Swip()
    {
        Vector2 distance = startPos - endpos;

        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            // Debug.Log("Horizontal swip");

            if (distance.x > 0) { Debug.Log("left"); }
            if (distance.x < 0) { Debug.Log("right"); }
        }

        else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            //  Debug.Log("vertical Swip");
            if (distance.y > 0) { Debug.Log("Down"); }
            if (distance.y < 0) { Debug.Log("up"); }
        }
    }

}
