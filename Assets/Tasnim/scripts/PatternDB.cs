using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "pattern Data Base", menuName = "PatternDataBase/Pattern")]

public class PatternDB : ScriptableObject {

    public int CurrentCountOfPatternInCertainDifficultyLevel;
    public List<List<PatternSO>> PatternDBList = new List<List<PatternSO>>();
    public List<TileType> segment = new List<TileType>(5);

    public int Count
    {
        get
        {
            return PatternDBList.Count;
        }
    }



    ////NO Need For this function in the current time 

    //int CurrentCountOfPatternInCertainDifficulty()
    //{
    //    for (int i = 0; i < Count; i++)
    //    {
    //        CurrentCountOfPatternInCertainDifficultyLevel = PatternDBList[i].Count;
    //    }
    //    return CurrentCountOfPatternInCertainDifficultyLevel;
    //}
    
   


    void onEnable()
    {
        //List<PatternSO> psoList = new List<PatternSO>();
        //PatternDBList[1] = psoList;
        //PatternDBList[2] = psoList;

        //  psoList.Add(AssetDatabase.FindAssets("t:" + typeof(PatternSO).Name));



    }

}
