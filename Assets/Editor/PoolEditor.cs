using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoolableDatabase))]
public class PoolEditor : Editor
{

    PoolableDatabase poolableDB;
    private void OnEnable()
    {
        poolableDB = (PoolableDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



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
            TileType ss = (TileType)ScriptableObject.CreateInstance("EnumValue");
            ss.name = "sss";
            poolableDB.prefabsDict.Add(ss, new PoolableObj());
        }



    }
}
