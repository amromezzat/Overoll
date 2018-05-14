using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectPool))]
public class PoolEditor : Editor
{

    ObjectPool objPool;

    private void OnEnable()
    {
        objPool = (ObjectPool)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        int i = 1;
        foreach (KeyValuePair<EnumValue, PrefabCount> entry in ObjectPool.instance.prefabsDict)
        {
            GUILayout.Label(i.ToString());
            //EditorGUILayout.TextArea(ObjectPool.instance.prefabsDict.Values);
        }


    }
}
