using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoolDatabase))]
public class PoolDBEditor : Editor
{

    public PoolDatabase poolableDB;
    Vector2 scrollPos = Vector2.zero;//list of prefabs scroll position
    bool removingAll = false;//remove all confirmation button

    //current editable values
    public PoolableType tileType;
    public GameObject parent;
    public GameObject prefab;
    public int instNum = 10;
    InteractablesDatabase interactablesDB;//tiles database to select from
    public int selectedTile = 0;

    private void OnEnable()
    {
        poolableDB = (PoolDatabase)target;

        string interactablesDBPath = "Assets/Resources/Database/InteractablesDatabase.asset";
        interactablesDB = (InteractablesDatabase)AssetDatabase.LoadAllAssetsAtPath(interactablesDBPath)[0];
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        //EditorGUILayout.Separator();

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
            GUILayout.MinHeight(160));
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

        selectedTile = EditorGUILayout.Popup("Label", selectedTile, 
            interactablesDB.interactablesNames.ToArray());
        tileType = interactablesDB[selectedTile];

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

        if (!prefab)
        {
            EditorGUILayout.HelpBox("Prefab must be set", MessageType.Warning);
            addEnabled = true;
        }

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
        string texture = "Assets/Resources/Textures/PoolableAssets/" + name + ".png";
        Texture2D inputTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
        if (!inputTexture)
            return;
        
        GUILayout.Label(inputTexture, GUILayout.MaxHeight(70), GUILayout.MaxWidth(70));
    }
}
