using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * NOTE: GUILayout uses default UI layout
 * Note: GUI allows for custom placement of UI elements
 */

public class RPGTool : EditorWindow {
    private int m_currentTab;

    //private const string m_charPrefabPath = "Assets/RPG Tool Assets/prefabs/character.prefab";
    private const string m_charPrefabPath = "Assets/Editor/RPG Tool Assets/prefabs/character.prefab";
    private string m_textField_tab1 = "";
    private string m_charName_tab2 = "";

    //Adds button to unitys window dropdown to open the following windows
    [MenuItem("Window/RPG Tool")]
    public static void ShowWindow()
    {
        //Opens the window
        EditorWindow mainWindow = EditorWindow.GetWindow(typeof(RPGTool));

        //Opens the character window in the same window as a tab
        //EditorWindow characterWindow = EditorWindow.GetWindow<RPGCharacterWindow>("Character", typeof(RPGTool));
    }

    private void OnGUI()
    {
        int previousTab = m_currentTab;

        //Creates and updates tabs
        m_currentTab = GUILayout.Toolbar(m_currentTab, new string[] { "General", "Character", "Test 3"});

        switch (m_currentTab)
        {
            case 0:
                DisplayTabOne();
                break;
            case 1:
                DisplayTabTwo();
                break;
        }

        //Checks if a tab is changed
        if (previousTab != m_currentTab)
        {
            //Used to prefent text fields from having the same displayed value when switching tabs
            GUI.FocusControl("");
        }
    }

    //Tab 1
    private void DisplayTabOne()
    {
        //Creates label
        GUILayout.Label("Test GUI", EditorStyles.boldLabel);
        //Creates editable text field
        m_textField_tab1 = EditorGUILayout.TextField("Text Field", m_textField_tab1);
    }

    //Tab 2
    private void DisplayTabTwo()
    {
        //Creates label
        GUILayout.Label("Properties", EditorStyles.boldLabel);

        //Draws and gets the "Name" text field
        m_charName_tab2 = EditorGUILayout.TextField("Name", m_charName_tab2);

        //Creates and checks if button is pressed
        if (GUILayout.Button("Create Character"))
        {
            //Debug messages to show if button is pressed
            Debug.Log("Editor Button Pressed");

            //Gets the prefab
            Object prefab = AssetDatabase.LoadAssetAtPath(m_charPrefabPath, typeof(GameObject));
            //Creates the prefab in the scene
            Object obj = PrefabUtility.InstantiatePrefab(prefab);

            //Names the object using data from the "Name" text field
            obj.name = m_charName_tab2;
        }
    }
}
