using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InteractablesDatabase : ScriptableObject
{

    public List<TileType> tileTypeList;

    [HideInInspector]
    public List<string> interactablesNames;

    public int Count
    {
        get
        {
            return tileTypeList.Count;
        }
    }

    TileType this[string name]
    {
        get
        {
            return tileTypeList[interactablesNames.IndexOf(name)];
        }
        set
        {
            tileTypeList[interactablesNames.IndexOf(name)] = value;
        }
    }



    public void Add(TileType tile)
    {
        if (!tileTypeList.Contains(tile))
            tileTypeList.Add(tile);
    }

    public bool Remove(TileType tile)
    {
        return tileTypeList.Remove(tile);
    }

    public void RemoveAt(int index)
    {
        tileTypeList.RemoveAt(index);
    }

}


