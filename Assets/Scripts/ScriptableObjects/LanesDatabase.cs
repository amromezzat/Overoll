using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanesDatabase", menuName = "Database/Lanes")]
public class LanesDatabase : ScriptableObject
{
    public TileConfig tc;

    private float laneWidth;//width of each seperate lane

    [SerializeField]
    List<LaneType> onGridLanes;
    [SerializeField]
    List<LaneType> gridLanes;

    private LaneType currentLane;
    private int currentLaneIndex;


    public LaneType CurrentLane
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

    public List<LaneType> GridLanes
    {
        get
        {
            return gridLanes;
        }
    }

    public List<LaneType> OnGridLanes
    {
        get
        {
            return onGridLanes;
        }
    }

    public float LaneWidth
    {
        get
        {
            return laneWidth;
        }

        set
        {
            laneWidth = value;
        }
    }

    //get lane by index

    public LaneType this[int index]
    {
        get
        {
            index %= onGridLanes.Count;
            return onGridLanes[index];
        }
    }

    private void OnEnable()
    {
        currentLane = gridLanes[2];
        currentLaneIndex = 2;
    }

    public void RecalculateLanesCenter()
    {
        laneWidth = tc.laneWidth;
        for(int i = 0; i < gridLanes.Count; i++)
        {
            gridLanes[i].laneCenter = (gridLanes[i].LaneNum - 2) * laneWidth;
        }
    }

    //get lane index by lane
    public int this[LaneType laneName]
    {
        get { return OnGridLanes.IndexOf(laneName); }
    }

    //return the next lane left
    //or the same lane if it were that last to the left
    public float GoLeft()
    {
        if (currentLaneIndex == 0)
        {
            return currentLane.laneCenter;
        }
        currentLane = OnGridLanes[--currentLaneIndex];
        return currentLane.laneCenter;
    }


    //return the next lane right
    //or the same lane if it were that last to the right
    public float GoRight()
    {
        if (currentLaneIndex == OnGridLanes.Count - 1)
        {
            return currentLane.laneCenter;
        }
        currentLane = OnGridLanes[++currentLaneIndex];
        return currentLane.laneCenter;
    }

    //Add a lane to the left
    public bool AddLeft()
    {
        //add a lane to the left if there is no more than 1 lane to the left
        if (OnGridLanes[0].LaneNum > 0)
        {
            LaneType newLeftLane = gridLanes[OnGridLanes[0].LaneNum - 1];
            OnGridLanes.Insert(0, newLeftLane);
            return true;
        }
        return false;
    }

    //Add a lane to the right
    public bool AddRight()
    {
        //add a lane to the right if there is no more than 1 lane to the right
        if (OnGridLanes[OnGridLanes.Count - 1].LaneNum < 4)
        {
            LaneType newRightLane = gridLanes[OnGridLanes[OnGridLanes.Count - 1].LaneNum + 1];
            onGridLanes.Add(newRightLane);
            return true;
        }
        return false;
    }

    //Remove a lane from the left
    public bool RemoveLeft()
    {
        //remove a lane from the left if there is at least two lanes to the left of the middle one
        if (OnGridLanes[0].LaneNum < 2)
        {
            OnGridLanes.RemoveAt(0);
            return true;
        }
        return false;
    }

    //Remove a lane from the right
    public bool RemoveRight()
    {
        //remove a lane from the right if there is at least two lanes to the right of the middle one
        if (OnGridLanes[OnGridLanes.Count - 1].LaneNum > 2)
        {
            OnGridLanes.RemoveAt(OnGridLanes.Count - 1);
            return true;
        }
        return false;
    }
}
