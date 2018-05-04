using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{

    public float maxTime;
    public float minSwipeDis;
    public WorkerConfig wc;

    float startTime;
    float endTime;

    Vector3 startPos;
    Vector3 endPos;

    float swipeDis;
    float swipeTime;

    private void Start()
    {

    }

    void Update()
    {
#if UNITY_EDITOR
        AndroidControls();
        WindowsControls();
#elif UNITY_ANDROID
        AndroidControls();
#elif UNITY_STANDALONE_WIN
        WindowsControls();
#endif
    }
    
    void WindowsControls()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            wc.onJump.Invoke();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            wc.onSlide.Invoke();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            wc.onRight.Invoke();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            wc.onLeft.Invoke();
        }
    }

    void AndroidControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //when screen touch starts record the time and position
            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
            }
            //when screen touch ends record the time and position
            //get the difference to determine if the touch
            //is considered a swipe and in what direction
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = touch.position;

                swipeDis = (endPos - startPos).magnitude;
                swipeTime = (endTime - startTime);

                if (swipeTime < maxTime && swipeDis > minSwipeDis)
                {
                    Swipe();
                }

            }
        }
    }

    void Swipe()
    {
        Vector2 distance = startPos - endPos;

        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            if (distance.x > 0)
            {
                wc.onLeft.Invoke();
            }
            if (distance.x < 0)
            {
                wc.onRight.Invoke();
            }
        }

        else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            if (distance.y > 0)
            {
                wc.onSlide.Invoke();
            }

            if (distance.y < 0)
            {
                wc.onJump.Invoke();
            }
        }
    }
}
