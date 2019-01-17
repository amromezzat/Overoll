/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public WorkerConfig wc;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    bool windowsAction;
    bool androidDragging;

    int doubleTapCount;
    float doubleTapTimer;

    const float doubleTapTime = 1.5f;

    void Start()
    {
        dragDistance = Screen.height * 5 / 100; //dragDistance is 5% height of the screen
        windowsAction = false;
        androidDragging = false;
        doubleTapCount = 0;
        doubleTapTimer = doubleTapTime;
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
        if (!Input.anyKeyDown)
        {
            windowsAction = false;
        }
        else if(windowsAction)
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            wc.onAddWorker.Invoke();
            windowsAction = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            wc.onJump.Invoke();
            windowsAction = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            wc.onSlide.Invoke();
            windowsAction = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            wc.onRight.Invoke();
            windowsAction = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            wc.onLeft.Invoke();
            windowsAction = true;
        }
    }

    void AndroidControls()
    {
        if (doubleTapCount > 0)
        {
            doubleTapTimer -= Time.deltaTime;
        }
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
                if (!androidDragging)
                {   //It's a tap as the drag distance is less than 10% of the screen height
                    doubleTapCount++;
                    if (doubleTapCount == 2)
                    {
                        if (doubleTapTimer > 0)
                        {
                            wc.onAddWorker.Invoke();
                        }
                        doubleTapCount = 0;
                        doubleTapTimer = doubleTapTime;
                    }
                }
                else
                {
                    doubleTapCount = 0;
                    doubleTapTimer = doubleTapTime;
                }
                androidDragging = false;
            }
        }
    }

    void AndroidDetermineTouchType(Touch touch)
    {
        if (androidDragging)
        {
            return;
        }

        lp = touch.position;  //last touch position.

        //Check if drag distance is greater than 10% of the screen height
        if ((lp - fp).magnitude > dragDistance)
        {
            androidDragging = true;
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
