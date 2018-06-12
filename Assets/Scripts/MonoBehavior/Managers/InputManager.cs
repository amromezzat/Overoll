using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public float minSwipeDis = 125;
    public WorkerConfig wc;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 movPos;


    float swipeDis;
    float moveDis;

    bool insideSwip;
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
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            wc.onJump.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            wc.onSlide.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            wc.onRight.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
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
            if (touch.phase == TouchPhase.Began && !insideSwip)
            {
                startPos = touch.position;

            }
 
            if (touch.phase == TouchPhase.Moved && !insideSwip)
            {
                insideSwip = true;
             
                movPos = touch.position;

                moveDis = (movPos - startPos).magnitude;

                if (moveDis > minSwipeDis)
                {
                    Debug.Log("but why");
                    Swipe();
                }

            }
            //when screen touch ends record the time and position
            //get the difference to determine if the touch
            //is considered a swipe and in what direction
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                insideSwip = false;
              
                //    endPos = touch.position;

                //    swipeDis = (endPos - startPos).magnitude;

                //    if (swipeDis > minSwipeDis)
                //    {
                //        Swipe();
                //    }

            }


        }

      
    }

    void Swipe()
    {
        // Vector2 delta = endPos - startPos;

        Vector2 delta = movPos - startPos;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x < 0)
            {
                wc.onLeft.Invoke();
                insideSwip = true;
            }
            else
            {
                wc.onRight.Invoke();
                insideSwip = true;
            }
        }

        else
        {
            if (delta.y < 0)
            {
                wc.onSlide.Invoke();
                insideSwip = true;
            }

            else
            {
                wc.onJump.Invoke();
                insideSwip = true;
            }
        }
    }
}
