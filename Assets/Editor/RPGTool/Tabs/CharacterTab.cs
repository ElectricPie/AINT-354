using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterTab : Tab {

    //Path to prefab
    private const string m_charPrefabPath = "Assets/RPGTool/prefabs/character.prefab";

    private string m_charName = "";
    private float m_speed = 10;

    private GameObject m_character;

    //Int to hold the selected value on tab 2
    private int m_selectedRadio_tab2 = 0;

    public CharacterTab()
    {
        m_tabName = "Character";
    }
     
    public override void DisplayTab()
    {
        //Creates label
        GUILayout.Label("Properties", EditorStyles.boldLabel);

        //Draws and gets the "Name" text field
        m_charName = EditorGUILayout.TextField("Name", m_charName);

        //Draws and gets the "Speed" slider
        m_speed = EditorGUILayout.Slider("Speed", m_speed, 1, 10);

        //Draws label
        GUILayout.Label("Add Test Script", EditorStyles.boldLabel);

        //Draws radio buttons and gets value
        m_selectedRadio_tab2 = GUILayout.SelectionGrid(m_selectedRadio_tab2, new string[] { "Yes", "No" }, 2);
        
        //Creates and checks if button is pressed
        if (GUILayout.Button("Create Character"))
        {
            //Checks if a character is already present in the scene
            if (FindCharacter() == null)
            {
                CreateCharBtn();
            }
            else
            {
                Debug.LogError("Character Script Already In Scene");
            }
        }
    }

    private void CreateCharBtn()
    {
        //Debug messages to show if button is presseda
        Debug.Log("Editor Button Pressed");

        GameObject charPrefab = CreateObject(m_charPrefabPath);

        //Names the object using data from the "Name" text field
        charPrefab.name = m_charName;
        //Sets the speed for the character
        charPrefab.GetComponent<RPGCharacterController>().speed = m_speed;

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

    private Object FindCharacter()
    {
        //Finds the first instance with the RPGCharacterController attached to it
        return GameObject.FindObjectOfType<RPGCharacterController>();
    }
}
