using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script is used to modify obstacles in patterns segement by segment
/// </summary>

[CustomEditor(typeof(PatternSO))]
public class PatternEditor : Editor
{
    int segmentNum;
    PatternSO currentInstance;
    void OnEnable()
    {
        currentInstance = target as PatternSO;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //GUILayout.Label("No. of Segments");
        segmentNum = EditorGUILayout.IntField("segements number", 0);
        Show();
     
        EditorUtility.SetDirty(currentInstance);
    }

    void Show()
    {
        for (int i = 0; i < currentInstance.Count; i++)
        {
            GUILayout.BeginVertical();
            {
                var segTemp = currentInstance[i];
                for (int j = 0; j < segTemp.Count; j++)
                {
                    var tileTemp = segTemp[j];
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label(tileTemp.name);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
        }

    }

    void Edit()
    {
        //for (int i = 0; i < segmentNum; i++)
        //{
        //    if (GUI.changed)
        //    {
        //        if (currentInstance.Count > segmentNum)
        //        {
        //            currentInstance.segmentList.RemoveRange(segmentNum-1, currentInstance.Count-segmentNum );
        //        }
        //        else
        //        {
        //            for (int i = 0; i < currentInstance.Count - segmentNum; i++)
        //            {
        //                currentInstance.segmentList.AddRange(currentInstance.Count, )
        //            }
        //        }
        //    }
        //}
    }
}
