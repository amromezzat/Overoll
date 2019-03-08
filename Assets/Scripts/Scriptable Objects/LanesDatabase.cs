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

/// <summary>
/// Active lanes that the workers can use, and the one 
/// the leader is currently following
/// </summary>
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
        set
        {
            currentLane = value;
            currentLaneIndex = onGridLanes.IndexOf(currentLane);
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

    //Get lane by index
    public LaneType this[int index]
    {
        get
        {
            index %= onGridLanes.Count;
            return onGridLanes[index];
        }
    }

    public void ResetLanes()
    {
        CurrentLane = gridLanes[2];
    }

    public void RecalculateLanesCenter()
    {
        laneWidth = tc.laneWidth;
        for(int i = 0; i < gridLanes.Count; i++)
        {
            gridLanes[i].laneCenter = (gridLanes[i].LaneNum - 2) * laneWidth;
        }
    }

    // Get lane index by lane
    public int this[LaneType laneName]
    {
        get { return OnGridLanes.IndexOf(laneName); }
    }

    // Return the next lane left
    // or the same lane if it were that last to the left
    public float GoLeft()
    {
        if (currentLaneIndex == 0)
        {
            return currentLane.laneCenter;
        }
        currentLane = OnGridLanes[--currentLaneIndex];
        return currentLane.laneCenter;
    }


    // Return the next lane right
    // or the same lane if it were that last to the right
    public float GoRight()
    {
        if (currentLaneIndex == OnGridLanes.Count - 1)
        {
            return currentLane.laneCenter;
        }
        currentLane = OnGridLanes[++currentLaneIndex];
        return currentLane.laneCenter;
    }

    // Add a lane to the left
    public bool AddLeft()
    {
        // Add a lane to the left if there is no more than one lane to the left
        if (OnGridLanes[0].LaneNum > 0)
        {
            LaneType newLeftLane = gridLanes[OnGridLanes[0].LaneNum - 1];
            OnGridLanes.Insert(0, newLeftLane);
            return true;
        }
        return false;
    }

    // Add a lane to the right
    public bool AddRight()
    {
        // Add a lane to the right if there is no more than one lane to the right
        if (OnGridLanes[OnGridLanes.Count - 1].LaneNum < 4)
        {
            LaneType newRightLane = gridLanes[OnGridLanes[OnGridLanes.Count - 1].LaneNum + 1];
            onGridLanes.Add(newRightLane);
            return true;
        }
        return false;
    }

    // Remove a lane from the left
    public bool RemoveLeft()
    {
        // Remove a lane from the left if there is at least two lanes to the left of the middle one
        if (OnGridLanes[0].LaneNum < 2)
        {
            OnGridLanes.RemoveAt(0);
            return true;
        }
        return false;
    }

    // Remove a lane from the right
    public bool RemoveRight()
    {
        // Remove a lane from the right if there is at least two lanes to the right of the middle one
        if (OnGridLanes[OnGridLanes.Count - 1].LaneNum > 2)
        {
            OnGridLanes.RemoveAt(OnGridLanes.Count - 1);
            return true;
        }
        return false;
    }
}
