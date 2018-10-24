﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * NOTE: GUILayout uses default UI layout
 * Note: GUI allows for custom placement of UI elements
 */

public class RPGTool : EditorWindow {
    private int m_currentTab;

    //Int to hold the selected value on tab 2
    private int m_selectedRadio_tab2 = 0;

    //Path to prefab
    private const string m_charPrefabPath = "Assets/RPGTool/prefabs/character.prefab";

    //Strings to hold the values when changing tabs
    private string m_textField_tab1 = "";
    private string m_charName_tab2 = "";

    private static bool m_dragginObject_tab3 = false;

    private SceneView sceneWindow;

    private void OnEnable()
    {
        //Gets the scene window
        sceneWindow = (SceneView)GetWindow(typeof(SceneView), true, "SceneView");

        SceneView.onSceneGUIDelegate += SceneGUI;
    }

    //Handels events in the scene window
    private static void SceneGUI(SceneView sceneView)
    {
        Event sceneEvent = Event.current;

    }

    //Adds button to unitys window dropdown to open the following windows
    [MenuItem("Window/RPG Tool")]
    public static void ShowWindow()
    {
        //Opens the window
        EditorWindow mainWindow = EditorWindow.GetWindow(typeof(RPGTool));
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
            case 2:
                DisplayTabThree();
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

        //Draws label
        GUILayout.Label("Add Test Script", EditorStyles.boldLabel);

        //Draws radio buttons and gets value
        m_selectedRadio_tab2 = GUILayout.SelectionGrid(m_selectedRadio_tab2, new string[] { "Yes", "No" }, 2);

        //Creates and checks if button is pressed
        if (GUILayout.Button("Create Character"))
        {
            CreateCharBtnTab2();
            
        }
    }

    private void DisplayTabThree()
    {
        //------
        //Code copyed from: https://gist.github.com/bzgeb/3800350

        //Gets events
        Event evt = Event.current;

        //Creates area to drop objects
        Rect drop_area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(drop_area, "Add Trigger");

        //Check if a drag event is happening
        switch (evt.type)
        {

            case EventType.DragUpdated:
            case EventType.DragPerform:

                //Does nothing if the mouse isnt in the drop area
                if (!drop_area.Contains(evt.mousePosition))
                    return;

                //Displays the mouse icon when an object is dragged on top
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;


                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object dragged_object in DragAndDrop.objectReferences)
                    {
                        // Do On Drag Stuff here
                        Debug.Log("Added");
                    }
                }

                break;
        }
        //End Copy
        //------
    }

    //----Tab 2 Methods----
    private void CreateCharBtnTab2()
    {
        //Debug messages to show if button is presseda
        Debug.Log("Editor Button Pressed");

        GameObject charPrefab = CreateObject(m_charPrefabPath);

        //Names the object using data from the "Name" text field
        charPrefab.name = m_charName_tab2;

        switch (m_selectedRadio_tab2)
        {
            case 0:
                //Adds the "AddingTest" script to the object
                charPrefab.AddComponent<AddingTest>();
                Debug.Log("Adding Component");
                break;
            case 1:
                Debug.Log("Not Adding Component");
                break;
        }
    }

    private GameObject CreateObject(string objPath)
    {
        //Gets the prefab
        Object prefab = AssetDatabase.LoadAssetAtPath(objPath, typeof(GameObject));
        //Creates the prefab in the scene
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        return obj;
    }

    //----Tab 2 End----
}
