using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterTab : Tab {

    //Path to prefab
    private const string m_charPrefabPath = "Assets/RPGTool/prefabs/PlayerCharacter.prefab";

    private string m_charName = "";
    private float m_speed = 10;

    private GameObject m_playerCharacter;

    //Int to hold the selected value on tab 2
    private int m_selectedRadio = 0;

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
        m_selectedRadio = GUILayout.SelectionGrid(m_selectedRadio, new string[] { "Yes", "No" }, 2);
        
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
        charPrefab.name = "[PC]" + m_charName;
        //Sets the variables for the character
        PlayerCharacter prefabController = charPrefab.GetComponent<PlayerCharacter>();
        prefabController.Name = m_charName;
        prefabController.Speed = m_speed;

        Debug.Log("Creationg Name: " + charPrefab.GetComponent<PlayerCharacter>().Name);

        switch (m_selectedRadio)
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
        return GameObject.FindObjectOfType<PlayerCharacter>();
    }
}
