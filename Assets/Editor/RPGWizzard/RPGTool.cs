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

    private SceneView m_sceneWindow;

    private Tab[] m_tabs;

    private string[] m_tabNames;

    private void OnEnable()
    {
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
        EditorWindow.GetWindow(typeof(RPGTool));
    }

    private void OnGUI()
    {
        int previousTab = m_currentTab;

        //Creates and updates tabs
        m_currentTab = GUILayout.Toolbar(m_currentTab, m_tabNames);

        switch (m_currentTab)
        {
            //Each case draws the tab and then sends it the size of the window
            case 0:
                m_tabs[0].DisplayTab();
                m_tabs[0].WindowSize = position;
                break;
            case 1:
                m_tabs[1].DisplayTab();
                m_tabs[1].WindowSize = position;
                break;
            case 2:
                m_tabs[2].DisplayTab();
                m_tabs[2].WindowSize = position;
                break;
        }

        //Checks if a tab is changed
        if (previousTab != m_currentTab)
        {
            //Used to prefent text fields from having the same displayed value when switching tabs
            GUI.FocusControl("");
        }
    }
}
