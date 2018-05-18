using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script is used to modify obstacles in patterns tile by tile and construct a segment
/// </summary>

[CustomEditor(typeof(PatternSO))]
public class PatternEditor : Editor
{
    int segmentNum;
    PatternSO currentInstance;

    InteractablesDatabase idb;
    Segment empty;
    int selected = 0;

    int[] interactableSelected;

    int segmentIndex;
    bool endAdd = false;

    void OnEnable()
    {
        interactableSelected = new int[5];
        currentInstance = target as PatternSO;
        idb = (InteractablesDatabase)AssetDatabase.LoadAllAssetsAtPath("Assets/Resources/Database/InteractablesDatabase.asset")[0];
        empty = new Segment(idb[0]);  // for edit
    }

    public override void OnInspectorGUI()
    {
        //EditorGUILayout.LabelField(interactableSelected.Length.ToString());
        // DrawDefaultInspector();
        //segmentNum = EditorGUILayout.IntField("segements number", 0);
        Show();
        EditorGUILayout.Separator();
        GUILayout.Label("", GUI.skin.horizontalSlider);
        ShowWithEdit();
        EditorGUILayout.Separator();
        GUILayout.Label("", GUI.skin.horizontalSlider);
        RemoveAll();

        EditorUtility.SetDirty(currentInstance); // to save the changes
    }

    void Show()
    {
        GUILayout.Label("Show Window", EditorStyles.boldLabel);
        if (currentInstance.segmentList.Count == 0)
        {
            GUILayout.Label("The Pattern is Empty");
        }
        for (int i = 0; i < currentInstance.Count; i++)
        {
            GUILayout.BeginHorizontal();
            {
                var segTemp = currentInstance[i];
                for (int j = 0; j < segTemp.Count; j++)
                {
                    var tileTemp = segTemp[j];
                    GUILayout.BeginVertical();
                    {
                        LoadPrefabTexture(tileTemp.name);
                    }
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Edit"))
                {
                    ShowWithEdit();
                }
                if (GUILayout.Button("Remove"))
                {
                    currentInstance.segmentList.RemoveAt(i);
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    void ShowWithEdit()
    {
        string[] options = new string[] { "Edit", "Add" };
        selected = EditorGUILayout.Popup("Add/Edit", selected, options);

        GUILayout.BeginVertical();
        {
            ShowSegment();

            GUILayout.BeginHorizontal();
            for (int i = 0; i < empty.Count; i++)
            {
                //EditorGUILayout.ObjectField(empty[i], typeof(TileType), false);
                LoadPrefabTexture(empty.ListOfTiles[i].name);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        switch (selected)
        {
            case 0:
                Edit();
                break;

            case 1:
                Add();
                break;
        }
    }

    void Edit()
    {
        EditorGUI.BeginDisabledGroup(currentInstance.segmentList.Count == 0);
        { 
        IndexSlider();
        GUILayout.BeginHorizontal();
        {
            
            if (GUILayout.Button("Edit"))
            {
                currentInstance[segmentIndex] = empty;
            }
        }
        GUILayout.EndHorizontal();
        }
        EditorGUI.EndDisabledGroup();
    }

    void Add()
    {
        GUILayout.BeginHorizontal();
        {
            endAdd = GUILayout.Toggle(endAdd, "Add at End");
            EditorGUI.BeginDisabledGroup(endAdd);
            {
                segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, empty.Count);
                if (segmentIndex > empty.Count)
                {
                    segmentIndex = empty.Count;
                }
            }
            EditorGUI.EndDisabledGroup();
            if (GUILayout.Button("Add"))
            {
                if (!endAdd)
                {
                    currentInstance.segmentList.Insert(segmentIndex, empty);
                }
                if (endAdd)
                {
                    currentInstance.segmentList.Add(empty);
                }
            }
        }
        GUILayout.EndHorizontal();
    }

    void RemoveAll()
    {
        GUILayout.BeginVertical();
        {
            GUILayout.Label("Danger Zone!", EditorStyles.boldLabel);
            if (GUILayout.Button("Remove All"))
            {
                currentInstance.segmentList.RemoveAll(listItem => true);
            }
        }
    }

    void ShowSegment()
    {
        GUILayout.BeginHorizontal();
        {
            for (int i = 0; i < 5; i++)
            {
                interactableSelected[i] = EditorGUILayout.Popup(interactableSelected[i], idb.interactablesNames.ToArray());
                empty[i] = idb[interactableSelected[i]];
            }
        }
        GUILayout.EndHorizontal();
    }

    void LoadPrefabTexture(string name)
    {
        string texture = "Assets/Resources/Textures/Interactables/" + name + ".png";
        Texture2D inputTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
        //if (!inputTexture)
        //    return;

        GUILayout.Label(inputTexture, GUILayout.MaxHeight(70), GUILayout.MaxWidth(80));
    }

    void IndexSlider()
    {
        segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, currentInstance.segmentList.Count);
        if (segmentIndex > currentInstance.segmentList.Count)
        {
            segmentIndex = currentInstance.segmentList.Count;
        }
    }

}