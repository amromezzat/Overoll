using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script is used to modify obstacles in patterns tile by tile and construct a segment
/// </summary>

[CustomEditor(typeof(Pattern))]
public class PatternEditor : Editor
{
    int segmentNum;
    Pattern pattern;

    InteractablesDatabase idb;//available interactables

    int selected = 0;

    int[] selectedInteractable = new int[5];

    Segment editAreaSeg;

    int segmentIndex;
    bool addAtEnd = true;

    Vector2 scrollPos = Vector2.zero;//Pattern Show Scroll

    void OnEnable()
    {
        pattern = target as Pattern;
        idb = AssetDatabase.LoadAssetAtPath<InteractablesDatabase>("Assets/Data/Database/InteractablesDatabase.asset");
        editAreaSeg = new Segment(idb[0]);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ShowPattern();
        EditorGUILayout.Separator();
        GUILayout.Label("", GUI.skin.horizontalSlider);
        EditablePattern();
        EditorGUILayout.Separator();
        GUILayout.Label("", GUI.skin.horizontalSlider);
    //    RemoveAll();

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(pattern);
    }

    /// <summary>
    /// Display Pattern
    /// </summary>
    void ShowPattern()
    {
        GUILayout.Label("Pattern", EditorStyles.boldLabel);
        if (pattern.segmentList.Count == 0)
        {
            GUILayout.Label("Empty!", EditorStyles.textField);
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true), GUILayout.MaxHeight(250),
            GUILayout.MinHeight(160));
        for (int i = 0; i < pattern.Count; i++)
        {
            GUILayout.BeginHorizontal("HelpBox");
            {
                var segTemp = pattern[i];
                GUILayout.BeginHorizontal("HelpBox");
                for (int j = 0; j < segTemp.Count; j++)
                {
                    var tileTemp = segTemp[j];
                    GUILayout.BeginVertical();
                    {
                        DisplayPrefabTexture(tileTemp.name);
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("HelpBox");

                if (GUILayout.Button(i.ToString(), EditorStyles.foldout))
                {
                    segmentIndex = i;
                    for (int j = 0; j < editAreaSeg.Count; j++)
                    {
                        selectedInteractable[j] = idb.interactablesNames.IndexOf(segTemp[j].name);
                        selected = 1;
                    }
                }

                if (GUILayout.Button("Remove"))
                {
                    pattern.segmentList.RemoveAt(i);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// just draws the Add/edit window to choose between both
    /// </summary>
    void EditablePattern()
    {
        string[] options = new string[] { "Add", "Edit" };
        selected = EditorGUILayout.Popup("Add/Edit", selected, options);

        if (selected == 0)
        {
            AddArea();
        }
        else if (selected == 1)
        {
            if (pattern.segmentList.Count > 0)
            {
                EditArea();
            }
            else
            {
                EditorGUILayout.HelpBox("Nothing to Edit!", MessageType.Info);
            }
        }
    }

    /// <summary>
    /// called when select to edit the segment
    /// </summary>
    void EditArea()
    {
        GUILayout.BeginHorizontal("HelpBox");
        {
            for (int i = 0; i < editAreaSeg.Count; i++)
            {
                GUILayout.BeginVertical();
                selectedInteractable[i] = EditorGUILayout.Popup(selectedInteractable[i], idb.interactablesNames.ToArray());
                editAreaSeg[i] = idb[selectedInteractable[i]];
                DisplayPrefabTexture(editAreaSeg[i].name);
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndHorizontal();

        EditorGUI.BeginDisabledGroup(pattern.segmentList.Count == 0);
        {
            IndexSlider();
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save"))
                {
                    pattern.segmentList[segmentIndex] = new Segment(editAreaSeg);
                }
            }
            GUILayout.EndHorizontal();
        }
        EditorGUI.EndDisabledGroup();
    }

    /// <summary>
    /// called when add is selected
    /// </summary>
    void AddArea()
    {
        Segment newSegment = new Segment(idb[0]);
        GUILayout.BeginHorizontal("HelpBox");
        {
            for (int i = 0; i < newSegment.Count; i++)
            {
                GUILayout.BeginVertical();
                selectedInteractable[i] = EditorGUILayout.Popup(selectedInteractable[i], idb.interactablesNames.ToArray());
                newSegment[i] = idb[selectedInteractable[i]];
                //EditorGUILayout.ObjectField(empty[i], typeof(TileType), false);
                DisplayPrefabTexture(newSegment.ListOfTiles[i].name);
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            addAtEnd = GUILayout.Toggle(addAtEnd, "Add at End");
            EditorGUI.BeginDisabledGroup(addAtEnd);
            {
                segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, pattern.segmentList.Count - 1);
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Add"))
            {
                if (addAtEnd)
                {
                    pattern.segmentList.Add(newSegment);
                    scrollPos += new Vector2(0, 100);
                }
                else
                {
                    pattern.segmentList.Insert(segmentIndex, newSegment);
                }
                for(int i = 0; i < newSegment.Count; i++)
                {
                    selectedInteractable[i] = 0;
                }
            }
        }
    }

    /// <summary>
    /// used to show the texture of the interactable object
    /// </summary>
    /// <param name="name">Get Prefab Texture by its name</param>
    void DisplayPrefabTexture(string name)
    {
        Texture2D inputTexture = (Texture2D)EditorGUIUtility.Load("PoolableAssets/" + name + ".png");
        GUILayout.Label(inputTexture, GUILayout.MaxHeight(70), GUILayout.MaxWidth(80));
    }

    /// <summary>
    /// to show the index as a range
    /// </summary>
    void IndexSlider()
    {
        segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, pattern.segmentList.Count - 1);
        if (segmentIndex > pattern.segmentList.Count)
        {
            segmentIndex = pattern.segmentList.Count;
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
            pattern.segmentList.RemoveAll(listItem => true);
        }
    }
}