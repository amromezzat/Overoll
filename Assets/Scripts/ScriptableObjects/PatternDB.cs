using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "pattern Data Base", menuName = "PatternDataBase/Pattern")]

public class PatternDB : ScriptableObject {


    public List<Difficulty> PatternDBList = new List<Difficulty>();
    // public List<TileType> segment = new List<TileType>(5);

    public int Count
    {
        get
        {
            return PatternDBList.Count;//PatternDBList.Count;
        }
    }

    public Difficulty this[int index]
    {
        get
        {
            return PatternDBList[index];
        }
        set
        {
            PatternDBList[index]= value;
        }
    }

}
