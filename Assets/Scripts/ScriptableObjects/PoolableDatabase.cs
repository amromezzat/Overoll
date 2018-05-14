using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lanes", menuName = "Config/Pool/PoolDatabase")]
public class PoolableDatabase : ScriptableObject
{
    public Dictionary<TileType, PoolableObj> prefabsDict = new Dictionary<TileType, PoolableObj>();
    int count = 0;
    List<TileType> keys;

    public PoolableObj this[TileType instType]
    {
        get
        {
            if (prefabsDict.ContainsKey(instType))
            {
                return prefabsDict[instType];
            }
            return new PoolableObj();
        }
    }

    public TileType this[int index]
    {
        get
        {
            return Keys[index];
        }
    }

    public int Count
    {
        get
        {
            count = prefabsDict.Count;
            return count;
        }
    }

    public List<TileType> Keys
    {
        get
        {
            List<TileType> keysList = new List<TileType>(Count);
            foreach (TileType key in keys)
            {
                keysList.Add(key);
            }
            return keysList;
        }
    }
}
