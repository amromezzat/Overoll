using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Segment 
{
     public List<EnumValue> segment= new List<EnumValue>(5);
    
    public EnumValue this [int i]
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

