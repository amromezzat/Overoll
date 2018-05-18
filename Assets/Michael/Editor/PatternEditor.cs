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

    int selected = 0;

    int[] interactableSelected;

    Segment seg;

    int segmentIndex;
    bool endAdd = false;

    void OnEnable()
    {
        interactableSelected = new int[5];
        currentInstance = target as PatternSO;
        idb = (InteractablesDatabase)AssetDatabase.LoadAllAssetsAtPath("Assets/Resources/Database/InteractablesDatabase.asset")[0];
        seg = new Segment(idb[0]);
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

    /// <summary>
    /// draws the pattern added
    /// </summary>
    void Show()
    {
        GUILayout.Label("Show Window", EditorStyles.boldLabel);
        if (currentInstance.segmentList.Count == 0)
        {
            GUILayout.Label("The Pattern is Empty");
        }
        for (int i = 0; i < currentInstance.Count; i++)
        {
                var segTemp = currentInstance[i];
            GUILayout.BeginHorizontal();
            {
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
                    segmentIndex = i;
                    for (int j = 0; j < seg.Count; j++)
                    {
                        interactableSelected[j] = idb.interactablesNames.IndexOf(segTemp[j].name);
                    }
                }
                if (GUILayout.Button("Remove"))
                {
                    currentInstance.segmentList.RemoveAt(i);
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// just draws the Add/edit window to choose between both
    /// </summary>
    void ShowWithEdit()
    {
        string[] options = new string[] { "Add", "Edit" };
        selected = EditorGUILayout.Popup("Add/Edit", selected, options);

        if (selected == 0)
        {
            Add();
        }
        else if (selected == 1)
        {
            if (currentInstance.segmentList.Count > 0)
            {
                DrawEdit();
            }
            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// called when select to edit the segment
    /// </summary>
    void DrawEdit()
    {
        GUILayout.BeginHorizontal();
        {
            for (int i = 0; i < seg.Count; i++)
            {
                GUILayout.BeginVertical();
                interactableSelected[i] = EditorGUILayout.Popup(interactableSelected[i], idb.interactablesNames.ToArray());
                seg[i] =  idb[interactableSelected[i]];
                //EditorGUILayout.ObjectField(empty[i], typeof(TileType), false);
                LoadPrefabTexture(seg[i].name);
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndHorizontal();

        EditorGUI.BeginDisabledGroup(currentInstance.segmentList.Count == 0);
        {
            IndexSlider();
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save"))
                {
                   currentInstance.segmentList[segmentIndex] = new Segment (seg);
                    
                }
            }
            GUILayout.EndHorizontal();
        }
        EditorGUI.EndDisabledGroup();
    }

    /// <summary>
    /// called when add chosen
    /// </summary>
    void Add()
    {
        Segment empty = new Segment(idb[0]);
        GUILayout.BeginHorizontal();
        {
            for (int i = 0; i < empty.Count; i++)
            {
                GUILayout.BeginVertical();
                interactableSelected[i] = EditorGUILayout.Popup(interactableSelected[i], idb.interactablesNames.ToArray());
                empty[i] = idb[interactableSelected[i]];
                //EditorGUILayout.ObjectField(empty[i], typeof(TileType), false);
                LoadPrefabTexture(empty.ListOfTiles[i].name);
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            endAdd = GUILayout.Toggle(endAdd, "Add at End");
            EditorGUI.BeginDisabledGroup(endAdd);
            {
                segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, currentInstance.segmentList.Count);
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

    /// <summary>
    /// used to show the texture of the interactable object
    /// </summary>
    /// <param name="name"></param>
    void LoadPrefabTexture(string name)
    {
        string texture = "Assets/Resources/Textures/Interactables/" + name + ".png";
        Texture2D inputTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
        GUILayout.Label(inputTexture, GUILayout.MaxHeight(70), GUILayout.MaxWidth(80));
    }

    /// <summary>
    /// to show the index as a range
    /// </summary>
    void IndexSlider()
    {
        segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, currentInstance.segmentList.Count);
        if (segmentIndex > currentInstance.segmentList.Count)
        {
            segmentIndex = currentInstance.segmentList.Count;
        }
    }

    /// <summary>
    /// function called to remove all segments in pattern
    /// </summary>
    void RemoveAll()
    {
        GUILayout.Label("Danger Zone!", EditorStyles.boldLabel);
        if (GUILayout.Button("Remove All"))
        {
            currentInstance.segmentList.RemoveAll(listItem => true);
        }
    }
}