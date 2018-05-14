using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PatternDB : ScriptableObject {


    public List<List<PatternSO>> PatternDataBase = new List<List<PatternSO>>();
    public List<TileType> segment = new List<TileType>(5); 


    void onEnable()
    {
        List<PatternSO> psoList = new List<PatternSO>(); 
         //  psoList.Add(AssetDatabase.FindAssets("t:" + typeof(PatternSO).Name));
    } 
   
}
