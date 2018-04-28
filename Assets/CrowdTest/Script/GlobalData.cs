using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a static class for accessible data across all monoscripts
//maybe replaced by scriptable object
public static class GlobalData
{
    //*****************************************************
    //Crowd static global variables
    //*****************************************************
    //mainly used in workers manager and position worker
    public static GameObject leader;
    public static Rigidbody leaderRb;
    public static int laneWidth;
    public static List<GameObject> workers;
    public static List<Rigidbody> workersRb = new List<Rigidbody>();
    public static int workersSepDis;
    public static int arrivalSlowingRad;
    public static int maxSepForce;
    public static int maxFolForce;
    public static int maxSpeed;
    public static Vector2 leaderPos = new Vector2();
    public static int aheadFollowPoint;
    //*****************************************************
}
