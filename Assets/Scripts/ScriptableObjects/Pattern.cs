using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Pattern", menuName = "Types/Pattern")]
public class Pattern : ScriptableObject
{
    public int difficulty;
    public float length;
    public float sectionLength;

    public List<Segment> segmentList;

    public Pattern()
    {
        segmentList = new List<Segment>();
    }


    public Segment this[int index]
    {
        get
        {
            if(index >= segmentList.Count)
            {
                return segmentList[0];
            }
            return segmentList[index];
        }
        set
        {
            segmentList[index] = value;
        }
    }
    public int Count
    {
        get
        {
            return segmentList.Count;
        }
    }

}
