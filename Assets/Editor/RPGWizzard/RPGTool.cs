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

    //Strings to hold the values when changing tabs
    private string m_textField_tab1 = "";

    private static bool m_dragginObject_tab3 = false;

    private SceneView m_sceneWindow;

    private Tab[] m_tabs;

    private string[] m_tabNames;

    private void OnEnable()
    {
        //Gets the scene window
        //m_sceneWindow = (SceneView)GetWindow(typeof(SceneView), false, "SceneView");

        //SceneView.onSceneGUIDelegate += SceneGUI;

        //Gets tabs
        m_tabs = new Tab[3] { new GeneralTab() ,new CharacterTab(), new AttributesTab() };

        //Gets the tabs names
        m_tabNames = new string[m_tabs.Length];

        for (int i = 0; i < m_tabNames.Length; i++)
        {
            m_tabNames[i] = m_tabs[i].TabName;
        }
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
        m_currentTab = GUILayout.Toolbar(m_currentTab, m_tabNames);//new string[] { m_tabs[0].TabName, m_tabs[1].TabName, m_tabs[2].TabName, "Test 3"});

        switch (m_currentTab)
        {
            case 0:
                m_tabs[0].DisplayTab();
                break;
            case 1:
                m_tabs[1].DisplayTab();
                break;
            case 2:
                m_tabs[2].DisplayTab();
                break;
            /*
            case 3:
                DisplayTabThree();
                break;
            */
        }

        //Checks if a tab is changed
        if (previousTab != m_currentTab)
        {
            //Used to prefent text fields from having the same displayed value when switching tabs
            GUI.FocusControl("");
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
}