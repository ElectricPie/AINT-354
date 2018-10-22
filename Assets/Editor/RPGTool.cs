using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * NOTE: GUILayout uses default UI layout
 * Note: GUI allows for custom placement of UI elements
 */

public class RPGTool : EditorWindow {
    int currentTab;

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
        

        //Creates and updates tabs
        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Test 1", "Test 2", "Test 3"});

        switch (currentTab)
        {
            case 0:
                DisplayTabOne();
                break;
            case 1:
                DisplayTabTwo();
                break;
        }        
    }

    //Tab 1
    private void DisplayTabOne()
    {
        string test = "Hello World!";

        //Creates label
        GUILayout.Label("Test GUI", EditorStyles.boldLabel);
        //Creates editable text field
        test = EditorGUILayout.TextField("Text Field", test);
    }

    //Tab 2
    private void DisplayTabTwo()
    {
        //Creates lable
        GUILayout.Label("Characters", EditorStyles.boldLabel);

        //Creates and checks if button is pressed
        if (GUILayout.Button("Create Character"))
        {
            //Debug messages to show if button is pressed
            Debug.Log("Editor Button Pressed");
        }
    }
}
