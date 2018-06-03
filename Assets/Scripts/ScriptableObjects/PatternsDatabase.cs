using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternDatabase", menuName = "Database/Patterns")]

public class PatternsDatabase: ScriptableObject {


    public List<Difficulty> PatternDBList;

    public PatternsDatabase()
    {
        PatternDBList = new List<Difficulty>();
    }

    public int Count
    {
        get
        {
            return PatternDBList.Count;
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

    public void Add(Difficulty diff)
    {
        PatternDBList.Add(diff);
    }

    public void Remove(Difficulty diff)
    {
        PatternDBList.Remove(diff);
    }

    public void RemoveAt(int index)
    {
        PatternDBList.RemoveAt(index);
    }

    public void RemoveRange(int index, int count)
    {
        PatternDBList.RemoveRange(index, count);
    }

}
