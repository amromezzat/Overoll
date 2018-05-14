using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a data type not a component on any gameobject
/// </summary>

[System.Serializable]
public class Segment : MonoBehaviour
{
     public List<TileType> segment= new List<TileType>(5);
    
    public TileType this [int i]
    {

        get
        {
            return segment[i];
        }

        set
        {
            segment[i] = value;
        }
    }
}

