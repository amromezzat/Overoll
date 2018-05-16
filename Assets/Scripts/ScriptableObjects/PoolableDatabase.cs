using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Lanes", menuName = "Config/Pool/PoolDatabase")]
public class PoolableDatabase : ScriptableObject
{
    public List<PoolableObj> poolableList;

    public int Count
    {
        get
        {
            return poolableList.Count;
        }
    }

    public PoolableObj this[int index]
    {
        get
        {
            return poolableList[index];
        }
        set
        {
            poolableList[index] = value;
        }
    }

    public PoolableObj this[TileType type]
    {
        get
        {
            return poolableList.FirstOrDefault(po => po.type == type);
        }
        set
        {
            int poIndex = poolableList.FindIndex(po => po.type == type);
            poolableList[poIndex] = value;
        }
    }

    public void Add(PoolableObj item)
    {
        if (!poolableList.Any(po => po.type == item.type))
            poolableList.Add(item);
    }

    public bool Remove(PoolableObj item)
    {
        return poolableList.Remove(item);
    }

    public void RemoveAt(int index)
    {
        poolableList.RemoveAt(index);
    }

    public bool Contains(TileType type)
    {
        if (poolableList.Any(po => po.type == type))
            return true;
        return false;
    }

    public void RemoveAll()
    {
        poolableList.RemoveAll(_none=>true);
    }
}
