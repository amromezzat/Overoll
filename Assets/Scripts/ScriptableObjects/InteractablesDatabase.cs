using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractablesDB", menuName = "Database/Interactables")]
public class InteractablesDatabase : ScriptableObject
{

    public List<PoolableType> tileTypeList;

    [HideInInspector]
    public List<string> interactablesNames;

    public int Count
    {
        get
        {
            return tileTypeList.Count;
        }
    }

    public PoolableType this[string name]
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

    public PoolableType this[int index]
    {
        get
        {
            return tileTypeList[index];
        }
    }



    public void Add(PoolableType tile)
    {
        if (!tileTypeList.Contains(tile))
            tileTypeList.Add(tile);
        interactablesNames.Add(tile.name);
    }

    public bool Remove(PoolableType tile)
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


