using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager2 : MonoBehaviour
{
    public WorkerConfig wc;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    bool windowsAction;
    bool androidAction;

    void Start()
    {
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
        windowsAction = false;
        androidAction = false;
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
            if (windowsAction)
            {
                return;
            }
            wc.onJump.Invoke();
            windowsAction = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (windowsAction)
            {
                return;
            }
            wc.onSlide.Invoke();
            windowsAction = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (windowsAction)
            {
                return;
            }
            wc.onRight.Invoke();
            windowsAction = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (windowsAction)
            {
                return;
            }
            wc.onLeft.Invoke();
            windowsAction = true;
        }
        else {
            windowsAction = false;
        }
    }

    void AndroidControls()
    {
        if (Input.touchCount > 0) // user is touching the screen
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                AndroidDetermineTouchType(touch);
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                AndroidDetermineTouchType(touch);
                //else
                //{   //It's a tap as the drag distance is less than 10% of the screen height
                //    Debug.Log("Tap");
                //}
                androidAction = false;
            }
        }
    }

    void AndroidDetermineTouchType(Touch touch)
    {
        if (androidAction)
        {
            return;
        }

        lp = touch.position;  //last touch position.

        //Check if drag distance is greater than 10% of the screen height
        if ((lp - fp).magnitude > dragDistance)
        {
            androidAction = true;
            //It's a drag
            //check if the drag is vertical or horizontal
            if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
            {   //If the horizontal movement is greater than the vertical movement...
                if ((lp.x > fp.x))  //If the movement was to the right)
                {   //Right swipe
                    wc.onRight.Invoke();
                }
                else
                {   //Left swipe
                    wc.onLeft.Invoke();
                }
            }
            else
            {   //the vertical movement is greater than the horizontal movement
                if (lp.y > fp.y)  //If the movement was up
                {   //Up swipe
                    wc.onJump.Invoke();
                }
                else
                {   //Down swipe
                    wc.onSlide.Invoke();
                }
            }
        }
    }
}
