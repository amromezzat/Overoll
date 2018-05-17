using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoolableDatabase))]
public class PoolEditor : Editor
{

    public PoolableDatabase poolableDB;
    Vector2 scrollPos = Vector2.zero;//list of prefabs scroll position
    bool removingAll = false;//remove all confirmation button

    //current editable values
    public TileType tileType;
    public GameObject parent;
    public GameObject prefab;
    public int instNum;


    private void OnEnable()
    {
        poolableDB = (PoolableDatabase)target;

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();

        DisplayCurrentPrefabs();

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();

        CreateNewPrefab();
    }

    void DisplayCurrentPrefabs()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Current Prefabs: ", EditorStyles.boldLabel);
        GUILayout.Label(poolableDB.Count.ToString());
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, 
            GUILayout.ExpandWidth(true), GUILayout.MaxHeight(250),
            GUILayout.MinHeight(120));

        //Display current included prefabs
        for (int i = 0; i < poolableDB.Count; i++)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            LoadPrefabText(poolableDB[i].Name);

            EditorGUILayout.BeginHorizontal("HelpBox");
            GUILayout.Label(i.ToString(), EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.LabelField(poolableDB[i].Name + "(" + poolableDB[i].count.ToString() + ")", EditorStyles.miniLabel);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Edit"))
            {
                tileType = poolableDB[i].type;
                parent = poolableDB[i].parent;
                prefab = poolableDB[i].prefab;
                instNum = poolableDB[i].count;
            }

            if (GUILayout.Button("Delete"))
            {
                poolableDB.RemoveAt(i);
                return;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        if (poolableDB.poolableList.Count == 0)
        {
            EditorGUILayout.LabelField("Empty", EditorStyles.textField);
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        RemoveAllButton();
    }

    void CreateNewPrefab()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Create New Prefab: ", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        tileType = (TileType)EditorGUILayout.ObjectField("Type", tileType, typeof(TileType), false);

        instNum = (int)EditorGUILayout.Slider("Instances Number", instNum, 1, 20);

        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        parent = (GameObject)EditorGUILayout.ObjectField("Parent", parent, typeof(GameObject), true);

        EditorGUILayout.Separator();

        AddEditButtons();
    }

    void AddEditButtons()
    {
        bool showAddNotEdit = true;
        bool addEnabled = false;

        //check if type already exists
        for (int i = 0; i < poolableDB.poolableList.Count; i++)
        {
            if (tileType && tileType.name == poolableDB.poolableList[i].Name)
            {
                showAddNotEdit = false;
            }
        }

        //check data correctness
        if (!tileType)
        {
            EditorGUILayout.HelpBox("Type must be set", MessageType.Warning);
            addEnabled = true;
        }
        if (!prefab)
        {
            EditorGUILayout.HelpBox("Prefab must be set", MessageType.Warning);
            addEnabled = true;
        }
        //if (instNum < 1)
        //{
        //    EditorGUILayout.HelpBox("Prefab instances must be at least 1", MessageType.Warning);
        //    addEnabled = true;
        //}

        EditorGUI.BeginDisabledGroup(addEnabled);

        if (showAddNotEdit && GUILayout.Button("Add"))
        {
            poolableDB.poolableList.Add(new PoolableObj(tileType, instNum, prefab, parent));
        }

        if (!showAddNotEdit && GUILayout.Button("Edit"))
        {
            poolableDB[tileType] = new PoolableObj(tileType, instNum, prefab, parent);
        }

        EditorGUI.EndDisabledGroup();
    }

    void RemoveAllButton()
    {
        bool removeAll = false;

        if (removingAll)
        {
            removingAll = false;
            removeAll = EditorUtility.DisplayDialog("Deleting all!", "Are you Certain you want to do this crazy act!?", "Yes I'm Crazy", "I'm Crazy but not now");
        }

        if (removeAll)
        {
            poolableDB.RemoveAll();
        }

        if (poolableDB.Count > 0 && GUILayout.Button("Remove All"))
        {
            removingAll = true;
        }
    }

    void LoadPrefabText(string name)
    {
        string texture = "Assets/Resources/Textures/Interactables/" + name + ".png";
        Texture2D inputTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
        if (!inputTexture)
            return;
        
        GUILayout.Label(inputTexture, GUILayout.MaxHeight(70), GUILayout.MaxWidth(70));
    }
}
