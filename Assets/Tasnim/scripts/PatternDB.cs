using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PatternDB : ScriptableObject {


    public List<List<PatternSO>> PatternDBList = new List<List<PatternSO>>();
    public List<TileType> segment = new List<TileType>(5);

    public int Count
    {
        get
        {
            return PatternDBList.Count;
        }
    }

    void onEnable()
    {
        List<PatternSO> psoList = new List<PatternSO>();
        //  psoList.Add(AssetDatabase.FindAssets("t:" + typeof(PatternSO).Name));
       
    } 
   
}
