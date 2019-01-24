using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SceneCharacterCreator : EditorWindow {

    public GameObject CharacterPrefab;

    private Rect m_mainBoxRect;

    //Sets the width that all tags will be
    private float m_tagLength = 70;
    //Sets the height that all properties will be
    private float m_propertyHeight = 20;
    //Sets the distance between each propertys 
    private float m_propertyGap = 21;

    private ScriptableCharacter m_character;

    [MenuItem("GameObject/RPG Wizzard/Characters/Create Character", false, 0)]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SceneCharacterCreator));
    }

    public void OnGUI()
    {
        //Updates the dimensions of the drawing space in relations to the window
        m_mainBoxRect = new Rect(20, 30, position.width - 50, position.height);
        //Creats the main box using the adaptable rect
        GUILayout.BeginArea(m_mainBoxRect);

        GUI.Label(new Rect(0, 0, m_tagLength, m_propertyHeight), "Character");

        //Drag and drop from: https://gist.github.com/bzgeb/3800350
        Event evt = Event.current;
        Rect drop_area = new Rect(m_tagLength, 0, m_mainBoxRect.width - m_tagLength, 20);
        if (m_character == null)
        {
            GUI.Box(drop_area, "Drag Character Here");
        }
        else
        {
            GUI.Box(drop_area, m_character.name);
        }

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                //Does nothing if the mouse isnt in the drop area
                if (!drop_area.Contains(evt.mousePosition))
                {
                    //Ends the area view if the droped asset is in the scroll view
                    GUILayout.EndArea();
                    return;
                }

                //Displays the mouse icon when an object is dragged on top
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    //Catches errors for incorrect object type
                    try
                    {
                        foreach (ScriptableCharacter dragged_object in DragAndDrop.objectReferences)
                        {
                            m_character = dragged_object;
                            Debug.Log(m_character.GetType());
                        }
                    }
                    catch (Exception ex) { }
                }
                break;
        }

        GUILayout.EndArea();

        if (GUI.Button(new Rect(20, m_propertyGap * 2.5f, m_mainBoxRect.width, m_propertyGap), "Create Character"))
        {
            //Checks what character type to create
            if (m_character is ScriptablePlayer)
            {
                //Creates the characters templates
                ScriptablePlayer characterTemplate = (ScriptablePlayer) m_character;

                //Creats the character in the scene
                GameObject charObj = (GameObject) PrefabUtility.InstantiatePrefab(CharacterPrefab);
                charObj.AddComponent<PlayerCharacter>();

                //Copys the template to the game object
                charObj.name = "Player[Class: " + characterTemplate.name + "]";
                charObj.GetComponent<PlayerCharacter>().Name = characterTemplate.name;
                charObj.GetComponent<PlayerCharacter>().Level = characterTemplate.level;
                charObj.GetComponent<PlayerCharacter>().MaxLevel = characterTemplate.maxLevel;
                charObj.GetComponent<PlayerCharacter>().ExperianceCurve = characterTemplate.experanceCurve;

                charObj.GetComponent<PlayerCharacter>().Attributes = AddAttributes(characterTemplate); ;
            }
            else if (m_character is ScriptableHostile)
            {
                //Creates the characters templates
                ScriptableHostile characterTemplate = (ScriptableHostile)m_character;

                //Creats the character in the scene
                GameObject charObj = (GameObject)PrefabUtility.InstantiatePrefab(CharacterPrefab);
                charObj.AddComponent<HostileCharacter>();

                //Copys the template to the game object
                charObj.name = "Player[Class: " + characterTemplate.name + "]";
                charObj.GetComponent<HostileCharacter>().Name = characterTemplate.name;
                charObj.GetComponent<HostileCharacter>().Level = characterTemplate.level;
                charObj.GetComponent<HostileCharacter>().AggroRange = characterTemplate.aggroRange;

                charObj.GetComponent<HostileCharacter>().Attributes = AddAttributes(characterTemplate);

                //Removes the camera from the hostile character
                DestroyImmediate(charObj.transform.GetChild(0).gameObject);
            }
        }
    }

    public List<Attribute> AddAttributes(ScriptableCharacter characterTemplate)
    {
        //Adds attributes to the character
        List<Attribute> attributes = new List<Attribute>();
        for (int i = 0; i < characterTemplate.attributes.Count; i++)
        {
            attributes.Add(
                new Attribute()
                {
                    Name = characterTemplate.attributes[i].name,
                    ShortName = characterTemplate.attributes[i].sName,
                    Disc = characterTemplate.attributes[i].disc,
                    BaseValue = characterTemplate.attributes[i].baseValue
                });
        }

        return attributes
    }
}
