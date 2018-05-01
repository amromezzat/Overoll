using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lanes", menuName = "Config/Lanes")]
public class Lanes : ScriptableObject
{
    public int laneWidth = 5;//width of each seperate lane
    public float laneCount = 3;//number of available lanes
    List<LaneName> onGridLanes;
    [SerializeField]
    List<LaneName> gridLanes;
    private LaneName currentLane;
    private LaneName lastLane;
    private int currentLaneIndex;

    public LaneName CurrentLane
    {
        get
        {
            return currentLane;
        }
    }

    public int CurrentLaneIndex
    {
        get
        {
            return currentLaneIndex;
        }
    }

    private void OnEnable()
    {
        //start with middle lane
        currentLaneIndex = 2;
        currentLane = gridLanes[2];
        onGridLanes = new List<LaneName>();
        onGridLanes.Add(currentLane);

        //initialize left and right lanes
        for (int i = 1; i < laneCount / 2; i++)
        {
            gridLanes[2 - i].laneCenter = -laneWidth * i;
            onGridLanes.Insert(0, gridLanes[2 - i]);
            gridLanes[2 + i].laneCenter = laneWidth * i;
            onGridLanes.Add(gridLanes[2 + i]);
        }

    }

    //get lane by index
    public LaneName this[int index]
    {
        get { return onGridLanes[index]; }
    }

    //get lane index by lane
    public int this[LaneName laneName]
    {
        get { return onGridLanes.IndexOf(laneName); }
    }

    //return the next lane left
    //or the same lane if it were that last to the left
    public float GoLeft()
    {
        if (currentLaneIndex == 0)
        {
            return currentLane.laneCenter;
        }
        lastLane = currentLane;
        currentLane = onGridLanes[--currentLaneIndex];
        return currentLane.laneCenter;
    }


    //return the next lane right
    //or the same lane if it were that last to the right
    public float GoRight()
    {
        if (currentLaneIndex == onGridLanes.Count - 1)
        {
            return currentLane.laneCenter;
        }
        lastLane = currentLane;
        currentLane = onGridLanes[++currentLaneIndex];
        return currentLane.laneCenter;
    }
}
