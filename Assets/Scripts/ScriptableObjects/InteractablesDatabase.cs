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
            int index = interactablesNames.IndexOf(name);
            tileTypeList[index] = value;
            interactablesNames[index] = value.name;
        }
    }

    TileType this[int index]
    {
        get
        {
            return tileTypeList[index];
        }
    }



    public void Add(TileType tile)
    {
        if (!tileTypeList.Contains(tile))
            tileTypeList.Add(tile);
        interactablesNames.Add(tile.name);
    }

    public bool Remove(TileType tile)
    {
        interactablesNames.Remove(tile.name);
        return tileTypeList.Remove(tile);
    }

    public void RemoveAt(int index)
    {
        tileTypeList.RemoveAt(index);
        interactablesNames.RemoveAt(index);
    }

}


