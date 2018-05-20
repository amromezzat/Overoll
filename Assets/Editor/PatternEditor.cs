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
    Pattern pattternSO;

    InteractablesDatabase idb;//available interactables

    int selected = 0;

    int[] interactableSelected;

    Segment seg;

    int segmentIndex;
    bool addAtEnd = false;

    void OnEnable()
    {
        interactableSelected = new int[5];
        pattternSO = target as Pattern;
        idb = (InteractablesDatabase)AssetDatabase.LoadAllAssetsAtPath("Assets/Resources/Database/InteractablesDatabase.asset")[0];
        seg = new Segment(idb[0]);
    }

    public override void OnInspectorGUI()
    {
        //EditorGUILayout.LabelField(interactableSelected.Length.ToString());
        // DrawDefaultInspector();
        //segmentNum = EditorGUILayout.IntField("segements number", 0);
        ShowPattern();
        EditorGUILayout.Separator();
        GUILayout.Label("", GUI.skin.horizontalSlider);
        EditablePattern();
        EditorGUILayout.Separator();
        GUILayout.Label("", GUI.skin.horizontalSlider);
        RemoveAll();

        EditorUtility.SetDirty(pattternSO); // to save the changes
    }

    /// <summary>
    /// Display Pattern
    /// </summary>
    void ShowPattern()
    {
        GUILayout.Label("Pattern", EditorStyles.boldLabel);
        if (pattternSO.segmentList.Count == 0)
        {
            GUILayout.Label("Empty!", EditorStyles.textField);
        }


        for (int i = 0; i < pattternSO.Count; i++)
        {
            GUILayout.BeginHorizontal("HelpBox");
            {
                var segTemp = pattternSO[i];
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
                    for (int j = 0; j < seg.Count; j++)
                    {
                        interactableSelected[j] = idb.interactablesNames.IndexOf(segTemp[j].name);
                        selected = 1;
                    }
                }

                if (GUILayout.Button("Remove"))
                {
                    pattternSO.segmentList.RemoveAt(i);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }
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
            if (pattternSO.segmentList.Count > 0)
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
            for (int i = 0; i < seg.Count; i++)
            {
                GUILayout.BeginVertical();
                interactableSelected[i] = EditorGUILayout.Popup(interactableSelected[i], idb.interactablesNames.ToArray());
                seg[i] = idb[interactableSelected[i]];
                DisplayPrefabTexture(seg[i].name);
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndHorizontal();

        EditorGUI.BeginDisabledGroup(pattternSO.segmentList.Count == 0);
        {
            IndexSlider();
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save"))
                {
                    pattternSO.segmentList[segmentIndex] = new Segment(seg);

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
        Segment empty = new Segment(idb[0]);
        GUILayout.BeginHorizontal("HelpBox");
        {
            for (int i = 0; i < empty.Count; i++)
            {
                GUILayout.BeginVertical();
                interactableSelected[i] = EditorGUILayout.Popup(interactableSelected[i], idb.interactablesNames.ToArray());
                empty[i] = idb[interactableSelected[i]];
                //EditorGUILayout.ObjectField(empty[i], typeof(TileType), false);
                DisplayPrefabTexture(empty.ListOfTiles[i].name);
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            addAtEnd = GUILayout.Toggle(addAtEnd, "Add at End");
            EditorGUI.BeginDisabledGroup(addAtEnd);
            {
                segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, pattternSO.segmentList.Count - 1);
                if (segmentIndex > empty.Count)
                {
                    segmentIndex = empty.Count;
                }
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Add"))
            {
                if (!addAtEnd)
                {
                    pattternSO.segmentList.Insert(segmentIndex, empty);
                }
                if (addAtEnd)
                {
                    pattternSO.segmentList.Add(empty);
                }
            }
        }
    }

    /// <summary>
    /// used to show the texture of the interactable object
    /// </summary>
    /// <param name="name"></param>
    void DisplayPrefabTexture(string name)
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
        segmentIndex = (int)EditorGUILayout.Slider("Index", segmentIndex, 0, pattternSO.segmentList.Count - 1);
        if (segmentIndex > pattternSO.segmentList.Count)
        {
            segmentIndex = pattternSO.segmentList.Count;
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
            pattternSO.segmentList.RemoveAll(listItem => true);
        }
    }
}