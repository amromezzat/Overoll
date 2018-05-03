using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Actipn
{
    Left, right, jump, Slide
}

public class TheInputManager:MonoBehaviour
{

    public float maxTime;
    public float minSwipDis;

    float startTime;
    float endTime;

    Vector3 startPos;
    Vector3 endpos;

    float swipdis;
    float swiptime;
    [HideInInspector]
    public UnityEvent OnLeft;
    [HideInInspector]
    public UnityEvent OnRight;
    [HideInInspector]
    public UnityEvent OnJump;
    [HideInInspector]
    public UnityEvent OnSlide;

    void Update()
    {
#if UNITY_EDITOR
       
           // OnJump.Invoke();
          //  OnLeft.Invoke();
          //  OnRight.Invoke();
          //  OnSlide.Invoke();
       
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
                    Swipe();
                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnJump.Invoke();
            Debug.Log("up arrow");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            OnSlide.Invoke();
            Debug.Log("down arrow");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnRight.Invoke();
            Debug.Log("right arrow");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnLeft.Invoke();
            Debug.Log("left arrow");
        }
        else if (Input.GetKey(KeyCode.Space))
        { Debug.Log("space"); }



#elif UNITY_ANDROID
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
                        Swipe();
                    }

                }
#elif UNITY_STANDALONE_WIN
      if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnJump.Invoke();
            Debug.Log("up arrow");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            OnSlide.Invoke();
            Debug.Log("down arrow");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnRight.Invoke();
            Debug.Log("right arrow");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnLeft.Invoke();
            Debug.Log("left arrow");
        }
        else if (Input.GetKey(KeyCode.Space))
        { Debug.Log("space"); }
#endif
    }




    void Swipe()
    {
        Vector2 distance = startPos - endpos;

        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            // Debug.Log("Horizontal swip");

            if (distance.x > 0)
            {
                Debug.Log("left");
                OnLeft.Invoke();
            }
            if (distance.x < 0)
            {
                OnRight.Invoke();
                Debug.Log("right");
            }
        }

        else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            //  Debug.Log("vertical Swip");
            if (distance.y > 0)
            {
                Debug.Log("Down");
                OnSlide.Invoke();
            }

            if (distance.y < 0)
            {
                OnJump.Invoke();
                Debug.Log("up");
            }
        }
    }


}
