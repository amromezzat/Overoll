using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Difficulty {

    public List<PatternSO> ListOfPatterns =new List<PatternSO>();
    
    public int Count
    {
        get
        {
          return  ListOfPatterns.Count;
        }
    }

    public PatternSO this [int index]
    {
        get
        {
            return ListOfPatterns[index];
        }
        set
        {
            ListOfPatterns[index] = value;
        }
    }

    public void RemoveAt(int index)
    {
        ListOfPatterns.RemoveAt(index);
    }

    public bool Contains(PatternSO pattern)
    {
        return ListOfPatterns.Contains(pattern);
    }

    public void Add(PatternSO pattern)
    {
        ListOfPatterns.Add(pattern);
    }
}
