using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class hamada : MonoBehaviour
{
    FlickGesture flick;
    // Use this for initialization
    void Start()
    {
        flick = this.GetComponent<FlickGesture>();
        flick.Flicked += OnFlick;        
    }


    void OnFlick(object sender, EventArgs ev)
    {
        
    }

}