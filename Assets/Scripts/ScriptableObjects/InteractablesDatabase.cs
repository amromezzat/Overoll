using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractablesDatabase", menuName = "Database/Interactables")]
public class InteractablesDatabase : ScriptableObject
{

    public List<PoolableType> poolableTypeList;
    public PoolableType Tile
    {
        get
        {
            return poolableTypeList[0];
        }
    }

    [HideInInspector]
    public List<string> interactablesNames;

    public int Count
    {
        get
        {
            return poolableTypeList.Count;
        }
    }

    public PoolableType this[string name]
    {
        get
        {
            return poolableTypeList[interactablesNames.IndexOf(name)];
        }
        set
        {
            int index = interactablesNames.IndexOf(name);
            poolableTypeList[index] = value;
            interactablesNames[index] = value.name;
        }
    }

    public PoolableType this[int index]
    {
        get
        {
            return poolableTypeList[index];
        }
    }



    public void Add(PoolableType tile)
    {
        if (!poolableTypeList.Contains(tile))
            poolableTypeList.Add(tile);
        interactablesNames.Add(tile.name);
    }

    public bool Remove(PoolableType tile)
    {
        interactablesNames.Remove(tile.name);
        return poolableTypeList.Remove(tile);
    }

    public void RemoveAt(int index)
    {
        poolableTypeList.RemoveAt(index);
        interactablesNames.RemoveAt(index);
    }

    public void SwapByIndex(int num1, int num2)
    {
        PoolableType poolableTemp = poolableTypeList[num1];
        poolableTypeList[num1] = poolableTypeList[num2];
        poolableTypeList[num2] = poolableTemp;

        string interactableNameTemp = interactablesNames[num1];
        interactablesNames[num1] = interactablesNames[num2];
        interactablesNames[num2] = interactableNameTemp;
    }

}


