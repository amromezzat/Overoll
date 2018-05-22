using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Difficulty {

    public List<Pattern> ListOfPatterns;

    public Difficulty()
    {
        ListOfPatterns = new List<Pattern>();
    }
    
    public int Count
    {
        get
        {
          return  ListOfPatterns.Count;
        }
    }

    public Pattern this [int index]
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

    public bool Contains(Pattern pattern)
    {
        return ListOfPatterns.Contains(pattern);
    }

    public void Add(Pattern pattern)
    {
        ListOfPatterns.Add(pattern);
    }
}
