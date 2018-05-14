using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectPool))]
[CustomEditor(typeof(PoolableDatabase))]
public class PoolEditor : Editor
{

    ObjectPool objPool;

    PoolableDatabase poolableDB;
    private void OnEnable()
    {
        objPool = (ObjectPool)target;
        poolableDB = (PoolableDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        int i = 1;
        foreach (KeyValuePair<EnumValue, PrefabCount> entry in ObjectPool.instance.prefabsDict)


        //List<EnumValue> keys = poolableDB.Keys;
        GUILayout.Label(poolableDB.Count.ToString());
        for(int i=0;i< poolableDB.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(i.ToString());
            EditorGUILayout.LabelField(poolableDB[i].ToString());
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add"))
        {
            EnumValue ss = (EnumValue)ScriptableObject.CreateInstance("EnumValue");
            ss.name = "sss";
            poolableDB.prefabsDict.Add(ss, new PoolableObj());
        }



    }
}
