using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PrefabCount {

    public int count;
    public GameObject prefab;
    public GameObject parent;

    PrefabCount(int _count, GameObject _prefab, GameObject _parent)
    {
        count = _count;
        prefab = _prefab;
        parent = _parent;
    }
}
