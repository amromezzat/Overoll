using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Segment 
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

