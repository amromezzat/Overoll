using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum Actipn
{
    Left,right,jump,Slide
}
public class TheInputManager {
    public UnityEvent OnLeft;
    public UnityEvent OnRight;
    public UnityEvent OnJump;
    public UnityEvent OnSlide;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump.Invoke();
        }

#elif UNITY_WEBGL

#else

#endif
    }
}
