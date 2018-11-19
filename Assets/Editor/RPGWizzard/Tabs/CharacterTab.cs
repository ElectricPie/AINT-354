using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterTab : Tab
{
    //Path to prefab
    private const string m_charPrefabPath = "Assets/RPGTool/prefabs/PlayerCharacter.prefab";

    //General character settings
    private string m_charName = "";
    private float m_speed = 10;

    //Player character settings
    private GameObject m_playerCharacter;
    private AnimationCurve m_experienceCurve = AnimationCurve.Linear(0, 0, 10, 10);
    private int m_level = 1;
    private int m_maxLevel = 5;

    //Scroll positions
    private Vector2 m_playerScrollPosition;
    private Vector2 m_nonPlayerScrollPosition;

    private int m_tabState = 0;

    private CharacterList m_characterList;

    public CharacterTab()
    {
        m_tabName = "Character";

        m_characterList = new CharacterList();
    }

    public override void DisplayTab()
    {
        //Player Characters
        GUILayout.Label("Player Characters", EditorStyles.boldLabel);

        //Draws the scroll view
        m_playerScrollPosition = GUILayout.BeginScrollView(m_playerScrollPosition, GUILayout.Width(200), GUILayout.Height(100));

        //Draws the new attribute button
        if (GUILayout.Button("New Player"))
        {
            ResetAllFields();
            m_tabState = 1;
        }

        for (int i = 0; i < m_characterList.GetPlayerCount(); i++)
        {
            if (GUILayout.Button(m_characterList.GetPlayerCharacter(i).Name))
            {

            }
        }

        //Ends the scroll view
        GUILayout.EndScrollView();

        //NPC
        GUILayout.Label("NPCs", EditorStyles.boldLabel);

        //Draws the scroll view
        m_nonPlayerScrollPosition = GUILayout.BeginScrollView(m_nonPlayerScrollPosition, GUILayout.Width(200), GUILayout.Height(100));

        //Draws the new attribute button
        if (GUILayout.Button("New NPC"))
        {
            ResetAllFields();
            m_tabState = 2;
        }

        for (int i = 0; i < m_characterList.GetHostileCount(); i++)
        {
            if (GUILayout.Button(m_characterList.GetHostileCharacter(i).Name))
            {

            }
        }

        //Ends the scroll view
        GUILayout.EndScrollView();

        switch(m_tabState) {
            case 1:
                General();
                PlayerCharacter();
                break;
            case 2:
                General();
                NonPlayerCharacter();
                break;
        }       
    }

    private void General()
    {
        //Draws and gets the "Name" text field
        m_charName = EditorGUILayout.TextField("Name", m_charName);

        //Draws and gets the "Speed" slider
        m_speed = EditorGUILayout.Slider("Speed", m_speed, 1, 10);
    }
    
    private void PlayerCharacter()
    {
        m_level = EditorGUILayout.IntField("Min Level", m_level);
        m_maxLevel = EditorGUILayout.IntField("Min Level", m_maxLevel);

        //Using Curves to display levels
        m_experienceCurve = EditorGUILayout.CurveField("Level Curve", m_experienceCurve, GUILayout.Height(200));

        //Creates the character template
        if (GUILayout.Button("Create Player"))
        {
            PlayerTemplate newPlayer = new PlayerTemplate
            {
                Name = m_charName,
                Speed = m_speed,
                Level = m_level,
                MaxLevel = m_maxLevel,
                ExperianceCurve = m_experienceCurve
            };

            m_characterList.AddCharacter(newPlayer);
        }
    }

    private void NonPlayerCharacter()
    {
        m_level = EditorGUILayout.IntField("Level", m_level);

        //Creates the character template
        if (GUILayout.Button("Create NPC"))
        {
            HostileTemplate newHostile = new HostileTemplate
            {
                Name = m_charName,
                Speed = m_speed,
                Level = m_level
            };

            m_characterList.AddCharacter(newHostile);
        }    
    }

    private void ResetAllFields()
    {
        m_charName = "";
        m_speed = 1;
        m_level = 1;
        m_maxLevel = 5;
        m_experienceCurve = AnimationCurve.Linear(0, 0, 10, 10);

        GUI.FocusControl("");
    }


    /*UNUSED CODE
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

    private void UNUSEDCODE()
    {
        /*
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
    */
}