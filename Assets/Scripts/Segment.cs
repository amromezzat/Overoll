using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents segment which consists of list of TileType
/// </summary>

[System.Serializable]
public class Segment
{
    /// <summary>
    /// This is not segment this is TileTypeList
    /// </summary>
    public List<TileType> ListOfTiles = new List<TileType>(5);

    public TileType this[int i]
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

