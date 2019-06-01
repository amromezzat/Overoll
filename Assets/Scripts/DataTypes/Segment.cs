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
/// This class represents segment which consists of a list of TileType
/// </summary>
[System.Serializable]
public class Segment
{
    /// <summary>
    /// This is not segment this is TileTypeList
    /// </summary>
    public List<PoolableType> ListOfTiles;

    public Segment()
    {
        ListOfTiles = new List<PoolableType>();
    }

    public Segment(PoolableType tile)
    {
        ListOfTiles = new List<PoolableType>();
        for (int i = 0; i < 5; i++)
        {
            ListOfTiles.Add(tile);
        }
    }

    public Segment(Segment segment)
    {
        ListOfTiles = new List<PoolableType>();
        for (int i = 0; i < 5; i++)
        {
            ListOfTiles.Add(segment.ListOfTiles[i]);
        }
    }

    public PoolableType this[int i]
    {

        get
        {
            return ListOfTiles[i];
        }

        set
        {
            ListOfTiles[i] = value;
        }
    }

    public int Count
    {
        get
        {
            return ListOfTiles.Count;
        }
    }
}

