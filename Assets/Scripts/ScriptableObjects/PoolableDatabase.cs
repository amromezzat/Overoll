using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lanes", menuName = "Config/Pool/PoolDatabase")]
public class PoolableDatabase : ScriptableObject
{

    public Dictionary<EnumValue, PoolableObj> prefabsDict = new Dictionary<EnumValue, PoolableObj>();
    int count = 0;
    List<EnumValue> keys;

    public PoolableObj this[EnumValue instType]
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

    public EnumValue this[int index]
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

    public List<EnumValue> Keys
    {
        get
        {
            List<EnumValue> keysList = new List<EnumValue>(Count);
            foreach (EnumValue key in keys)
            {
                keysList.Add(key);
            }
            return keysList;
        }
    }
}
