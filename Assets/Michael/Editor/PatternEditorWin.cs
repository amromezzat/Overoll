using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PatternEditorWin : EditorWindow
{
    int difficulty = 1;
    int noOfsegements = 1;
    int segmentSize = 5;

    GameObject obj;


    [MenuItem("Window/PatternEditor")]
    public static void Init()
    {
        PatternEditorWin window = (PatternEditorWin)GetWindow<PatternEditorWin>("Pattern Editor");
        window.Show();

    }

    private void OnGUI()
    {
        GUILayout.Label("This is used to determine the Pattern shape", EditorStyles.boldLabel);
        difficulty = EditorGUILayout.IntField("Difficulty of pattern", difficulty);

        noOfsegements = EditorGUILayout.IntField("No. of segements", noOfsegements);
        CreateList(noOfsegements);



    }

    void CreateList(int no)
    {
        GameObject[] arr = new GameObject[segmentSize];
        for (int i = 0; i < no; i++)
        {
            GameObject temp;
            for (int j = 0; j < segmentSize; j++)
            {
                // EditorGUILayout.BeginHorizontal();
                temp = (GameObject)EditorGUILayout.ObjectField(obj, typeof(GameObject), false);
                arr[j] = temp;
                // EditorGUILayout.EndHorizontal();

            }
        }
    }





}
