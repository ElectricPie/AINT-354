using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CharacterTab : Tab
{


    //Player character 
    private Rect m_playerScrollRect;
    private Vector2 m_playerScrollPos;

    private int m_newPlayerMaxLevel;

    private List<ScriptablePlayer> m_playerCharacters;

    private AnimationCurve m_newPlayerExpCurve;

    private string m_playerCharPath = "Assets/RPGWizzard/Characters/Players";

    //Hostile character 
    private Rect m_hostileScrollRect;
    private Vector2 m_hostileScrollPos;

    private int m_newHostileAggroRange;

    private List<ScriptableHostile> m_hostileCharacters;

    private string m_HostileCharPath = "Assets/RPGWizzard/Characters/Hostiles";

    //Edit Character
    private string m_editCharName;
    private string m_editCharDisc;
    private int m_editCharLevel;
    //-Player
    private int m_editCharMaxLevel;
    private AnimationCurve m_editCharExpCurve;
    //-Hostile
    private int m_editAggroRange;

    private int m_characterToEdit;
    private int m_tabState;


    //General
    private int m_characterType = 0;
    private int m_newCharStartingLevel;

    private ScriptableObjUtil m_scriptObjUtill;

    private Rect m_mainBoxRect;
    private Rect m_charScrollRect;
    private Rect m_propertiesBoxRect;

    private string m_newCharName;
    private string m_newCharDisc;

    private List<ScriptableAttribute> m_newAttributes;
    private Rect m_attributeScrollRect;
    private Vector2 m_attributeScrollPos;

    //Sets the width that all tags will be
    private float m_tagLength = 90;
    //Sets the height that all properties will be
    private float m_propertyHeight = 20;
    //Sets the distance between each propertys 
    private float m_propertyGap = 21;
    private float m_fieldWidth;

    public CharacterTab()
    {
        m_tabName = "Characters";

        m_scriptObjUtill = new ScriptableObjUtil();

        m_playerCharacters = new List<ScriptablePlayer>();
        m_hostileCharacters = new List<ScriptableHostile>();

        m_newAttributes = new List<ScriptableAttribute>();

        m_newPlayerExpCurve = AnimationCurve.Linear(0, 0, 50, 50);
    }

    public override void DisplayTab()
    {
        GetCharacters();

        //Updates the dimensions of the drawing space in relations to the window
        m_mainBoxRect = new Rect(20, 30, m_windowSize.width - 40, m_windowSize.height - 50);
        //Creats the main box using the adaptable rect
        GUILayout.BeginArea(m_mainBoxRect);
        //Updates the width of the fields which change with the main boxs width
        m_fieldWidth = m_propertiesBoxRect.width - m_tagLength;

        //Draws the UI
        //Creates a tool bar which is used to select what type of characters the list should show
        m_characterType = GUI.Toolbar(new Rect(0, 0, 200, 20), m_characterType, new string[] { "Player", "Hostile" });

        //Draws the player/hostile list depending on the toolbar selection
        if (m_characterType == 0)
        {
            DrawCharacterList(m_playerCharacters, ref m_playerScrollRect, ref m_playerScrollPos, 1);
        }
        else
        {
            DrawCharacterList(m_hostileCharacters, ref m_hostileScrollRect, ref m_hostileScrollPos, 2);
        }
        //Draws the properties box
        DrawPropertiesBox();

        //Ends the main box
        GUILayout.EndArea();
    }

    private void DrawCharacterList<charType>(List<charType> characters, ref Rect scrollRect, ref Vector2 scrollPos, int editState) where charType : ScriptableCharacter
    {
        //Updates the dimensions for the player scroll view
        scrollRect = new Rect(0, 22, 200, m_mainBoxRect.height - 22);
        //Draws the player characters list
        scrollPos = GUI.BeginScrollView(scrollRect, scrollPos, new Rect(0, 0, scrollRect.width - 20, characters.Count * 20), false, true);

        if (GUI.Button(new Rect(0, 0, scrollRect.width - 20, 20), "New Character"))
        {
            m_tabState = 0;
        }

        //Debug buttons to check scroll view size
        for (int i = 0; i < characters.Count; i++)
        {
            //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
            if (GUI.Button(new Rect(0, i * 20 + 21, scrollRect.width - 20, 20), characters[i].name))
            {
                m_characterToEdit = i;
                m_tabState = editState;
                if (m_tabState == 1)
                {
                    UpdatePlayerEdit();
                }
                else if (m_tabState == 2)
                {
                    UpdateHostileEdit();
                }
            }
        }


        GUI.EndScrollView();
    }


    private void DrawPropertiesBox()
    {
        //Creates the dimensions for the characters properties box
        m_propertiesBoxRect = new Rect(210, 0, m_mainBoxRect.width - 220, m_mainBoxRect.height);
        //Drawns the properties of the character 
        GUILayout.BeginArea(m_propertiesBoxRect);

        switch (m_tabState)
        {
            //New character
            case 0:
                DrawGeneralProperties();

                if (m_characterType == 0)
                {
                    DrawPlayerProperties();

                    if (GUI.Button(new Rect(0, m_propertyGap * 16, m_propertiesBoxRect.width, m_propertyGap), "Create Character"))
                    {
                        CreateNewPlayerChar();
                    }
                }
                else
                {
                    DrawHostlieProperties();

                    if (GUI.Button(new Rect(0, m_propertyGap * 10, m_propertiesBoxRect.width, m_propertyGap), "Create Character"))
                    {
                        CreateNewHostileChar();
                    }
                }
                break;
            //Edit player character
            case 1:
                DrawPlayerEditPropeties();
                break;
            case 2:
                DrawHostileEditPropeties();
                break;
        }
        //Ends the properties box
        GUILayout.EndArea();
    }

    private void DrawGeneralProperties()
    {
        //Name lable and field
        GUI.Label(new Rect(0, m_propertyGap * 0, m_tagLength, m_propertyHeight), "Name");
        m_newCharName = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 0, m_fieldWidth, m_propertyHeight), m_newCharName);

        //Discription lable and field
        GUI.Label(new Rect(0, m_propertyGap * 1, m_tagLength, m_propertyHeight), "Discription");
        m_newCharDisc = GUI.TextArea(new Rect(m_tagLength, m_propertyGap * 1, m_fieldWidth, 60), m_newCharDisc);

        DrawAttributeScroll();
    }

    private void DrawAttributeScroll()
    {
        GUI.Label(new Rect(0, m_propertyGap * 4, m_tagLength, m_propertyHeight), "Attributes");
        //Updates the dimensions for the characters attributes scroll view
        m_attributeScrollRect = new Rect(m_tagLength, m_propertyGap * 4, m_fieldWidth, 80);
        //Draws the characters attributes
        m_attributeScrollPos = GUI.BeginScrollView(m_attributeScrollRect, m_attributeScrollPos, new Rect(0, 0, m_attributeScrollRect.width - 20, m_newAttributes.Count * 20 + 20), false, true);

        //Drag and drop from: https://gist.github.com/bzgeb/3800350
        Event evt = Event.current;
        Rect drop_area = new Rect(0, 0, m_attributeScrollRect.width - 20, 20);
        GUI.Box(drop_area, "Add Attribute");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                //Does nothing if the mouse isnt in the drop area
                if (!drop_area.Contains(evt.mousePosition))
                {
                    //Ends the scroll view if the droped asset is in the scroll view
                    GUI.EndScrollView();
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
                        foreach (ScriptableAttribute dragged_object in DragAndDrop.objectReferences)
                        {
                            if (!CheckIfHasAttribute(dragged_object))
                            {
                                AddAttribute(dragged_object);
                            }
                            else
                            {
                                Debug.LogWarning("Character already has attribute: " + dragged_object.name);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                break;
        }

        //Draws the attributes attached to the character
        for (int i = 0; i < m_newAttributes.Count; i++)
        {
            //Replace with box
            if (GUI.Button(new Rect(0, i * 20 + 21, m_attributeScrollRect.width - 50, 20), m_newAttributes[i].name)) { 

            }

            if (GUI.Button(new Rect(m_attributeScrollRect.width - 50, i * 20 + 21, 30, 20), "X"))
            {
                m_newAttributes.RemoveAt(i);
            }
        }

        GUI.EndScrollView();
    }

    private bool CheckIfHasAttribute(ScriptableAttribute attribute)
    {
        for (int i = 0; i < m_newAttributes.Count; i++)
        {
            if (attribute == m_newAttributes[i])
            {
                return true;
            }
        }

        return false;
    }

    private void AddAttribute(ScriptableAttribute attribute)
    {
        m_newAttributes.Add(attribute);
    }


    private void DrawPlayerProperties()
    {
        //Starting level lable and field
        GUI.Label(new Rect(0, m_propertyGap * 8, m_tagLength, m_propertyHeight), "Starting Level");
        m_newCharStartingLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 8, m_fieldWidth, m_propertyHeight), m_newCharStartingLevel);

        //Max Level lable and field
        GUI.Label(new Rect(0, m_propertyGap * 9, m_tagLength, m_propertyHeight), "Max Level");
        m_newPlayerMaxLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 9, m_fieldWidth, m_propertyHeight), m_newPlayerMaxLevel);

        //Experiance curve lable and field
        GUI.Label(new Rect(0, m_propertyGap * 10, m_tagLength, m_propertyHeight), "Exp Curve");
        m_newPlayerExpCurve = EditorGUI.CurveField(new Rect(m_tagLength, m_propertyGap * 10, m_fieldWidth, m_propertyHeight * 6), m_newPlayerExpCurve, Color.green, new Rect(0, 0, m_newPlayerMaxLevel, 50));
    }


    private void CreateNewPlayerChar()
    {
        //Creates a player character scriptable object
        ScriptablePlayer newPlayerChar = ScriptableObject.CreateInstance<ScriptablePlayer>();
        //Assigns values to the new player character object 
        newPlayerChar.name = m_newCharName;
        newPlayerChar.discription = m_newCharDisc;
        newPlayerChar.attributes = m_newAttributes;
        newPlayerChar.level = m_newCharStartingLevel;
        newPlayerChar.maxLevel = m_newPlayerMaxLevel;
        newPlayerChar.experanceCurve = m_newPlayerExpCurve;

        //Creates the new character as a scriptable object 
        m_scriptObjUtill.CreateNewScriptableObj(newPlayerChar, m_newCharName, m_playerCharPath + "/");
    }

    private void CreateNewHostileChar()
    {
        //Creates a hostile character scriptable object
        ScriptableHostile newHostileChar = ScriptableObject.CreateInstance<ScriptableHostile>();
        //Assigns values to the new hostile character object 
        newHostileChar.name = m_newCharName;
        newHostileChar.discription = m_newCharDisc;
        newHostileChar.attributes = m_newAttributes;
        newHostileChar.level = m_newCharStartingLevel;
        newHostileChar.aggroRange = m_newHostileAggroRange;

        //Creates the new character as a scriptable object 
        m_scriptObjUtill.CreateNewScriptableObj(newHostileChar, m_newCharName, m_HostileCharPath + "/");
    }


    private void DrawHostlieProperties()
    {
        //Aggro range lable and field
        GUI.Label(new Rect(0, m_propertyGap * 8, m_tagLength, m_propertyHeight), "Level");
        m_newCharStartingLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 8, m_fieldWidth, m_propertyHeight), m_newCharStartingLevel);

        //Aggro range lable and field
        GUI.Label(new Rect(0, m_propertyGap * 9, m_tagLength, m_propertyHeight), "Aggro Range");
        m_newHostileAggroRange = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 9, m_fieldWidth, m_propertyHeight), m_newHostileAggroRange);
    }


    private void GetCharacters()
    {
        GetPlayerChar();
        GetHostileChar();
    }

    private void GetPlayerChar()
    {
        //Gets any scriptable player objects from storage
        m_playerCharacters = m_scriptObjUtill.GetScriptableObjs<ScriptablePlayer>(m_playerCharPath);
    }

    private void GetHostileChar()
    {
        //Gets any scriptable hostile objects from storage
        m_hostileCharacters = m_scriptObjUtill.GetScriptableObjs<ScriptableHostile>(m_HostileCharPath);
    }


    private void UpdatePlayerEdit()
    {
        //Updates the edit section with the needed character data
        m_editCharName = m_playerCharacters[m_characterToEdit].name;
        m_editCharDisc = m_playerCharacters[m_characterToEdit].discription;
        m_editCharLevel = m_playerCharacters[m_characterToEdit].level;
        m_editCharMaxLevel = m_playerCharacters[m_characterToEdit].maxLevel;
        m_editCharExpCurve = m_playerCharacters[m_characterToEdit].experanceCurve;

        //Pulls foucs away allowing values to reset
        GUI.FocusControl("");
    }

    private void UpdateHostileEdit()
    {
        //Updates the edit section with the needed character data
        m_editCharName = m_hostileCharacters[m_characterToEdit].name;
        m_editCharDisc = m_hostileCharacters[m_characterToEdit].discription;
        m_editCharLevel = m_hostileCharacters[m_characterToEdit].level;
        m_editAggroRange = m_hostileCharacters[m_characterToEdit].aggroRange;

        //Pulls foucs away allowing values to reset
        GUI.FocusControl("");
    }


    private void DrawPlayerEditPropeties()
    {
        GUI.Label(new Rect(0, m_propertyGap * 0, 200, m_propertyHeight), "Edit Player Character", EditorStyles.boldLabel);

        //Name lable and field
        GUI.Label(new Rect(0, m_propertyGap * 1, m_tagLength, m_propertyHeight), "Name");
        m_editCharName = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 1, m_fieldWidth, m_propertyHeight), m_editCharName);

        //Discription lable and field
        GUI.Label(new Rect(0, m_propertyGap * 2, m_tagLength, m_propertyHeight), "Discription");
        m_editCharDisc = GUI.TextArea(new Rect(m_tagLength, m_propertyGap * 2, m_fieldWidth, 60), m_editCharDisc);

        //Starting level lable and field
        GUI.Label(new Rect(0, m_propertyGap * 5, m_tagLength, m_propertyHeight), "Starting Level");
        m_editCharLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 5, m_fieldWidth, m_propertyHeight), m_editCharLevel);

        //Max Level lable and field
        GUI.Label(new Rect(0, m_propertyGap * 6, m_tagLength, m_propertyHeight), "Max Level");
        m_editCharMaxLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 6, m_fieldWidth, m_propertyHeight), m_editCharMaxLevel);

        //Experiance curve lable and field
        GUI.Label(new Rect(0, m_propertyGap * 7, m_tagLength, m_propertyHeight), "Exp Curve");
        m_editCharExpCurve = EditorGUI.CurveField(new Rect(m_tagLength, m_propertyGap * 7, m_fieldWidth, m_propertyHeight * 6), m_editCharExpCurve, Color.green, new Rect(0, 0, m_editCharMaxLevel, 50));

        if (GUI.Button(new Rect(0, m_propertyGap * 13, m_propertiesBoxRect.width, m_propertyGap), "Update Character"))
        {
            SavePlayerObj();

            //Returns the tab to the new character creation
            m_tabState = 0;
        }

        if (GUI.Button(new Rect(0, m_propertyGap * 14, m_propertiesBoxRect.width, m_propertyGap), "Cancel"))
        {
            //Returns the tab to the new character creation
            m_tabState = 0;
        }

        if (GUI.Button(new Rect(0, m_propertyGap * 16, m_propertiesBoxRect.width, m_propertyGap), "Delete"))
        {
            DeleteCharacter(m_playerCharPath + "/" + m_playerCharacters[m_characterToEdit].name + ".asset");

            //Returns the tab to the new character creation
            m_tabState = 0;
        }
    }

    private void DrawHostileEditPropeties()
    {
        GUI.Label(new Rect(0, m_propertyGap * 0, 200, m_propertyHeight), "Edit Hostile Character", EditorStyles.boldLabel);

        //Name lable and field
        GUI.Label(new Rect(0, m_propertyGap * 1, m_tagLength, m_propertyHeight), "Name");
        m_editCharName = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 1, m_fieldWidth, m_propertyHeight), m_editCharName);

        //Discription lable and field
        GUI.Label(new Rect(0, m_propertyGap * 2, m_tagLength, m_propertyHeight), "Discription");
        m_editCharDisc = GUI.TextArea(new Rect(m_tagLength, m_propertyGap * 2, m_fieldWidth, 60), m_editCharDisc);

        //Level lable and field
        GUI.Label(new Rect(0, m_propertyGap * 5, m_tagLength, m_propertyHeight), "Level");
        m_editCharLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 5, m_fieldWidth, m_propertyHeight), m_editCharLevel);

        //Aggro range lable and field
        GUI.Label(new Rect(0, m_propertyGap * 6, m_tagLength, m_propertyHeight), "Aggro Range");
        m_editAggroRange = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 6, m_fieldWidth, m_propertyHeight), m_editAggroRange);

        if (GUI.Button(new Rect(0, m_propertyGap * 7, m_propertiesBoxRect.width, m_propertyGap), "Update Character"))
        {
            SaveHostileObj();

            //Returns the tab to the new character creation
            m_tabState = 0;
        }

        if (GUI.Button(new Rect(0, m_propertyGap * 8, m_propertiesBoxRect.width, m_propertyGap), "Cancel"))
        {
            //Returns the tab to the new character creation
            m_tabState = 0;
        }

        if (GUI.Button(new Rect(0, m_propertyGap * 10, m_propertiesBoxRect.width, m_propertyGap), "Delete"))
        {
            DeleteCharacter(m_HostileCharPath + "/" + m_hostileCharacters[m_characterToEdit].name + ".asset");

            //Returns the tab to the new character creation
            m_tabState = 0;
        }
    }


    private void SavePlayerObj()
    {
        //Renames the file
        m_scriptObjUtill.ChangeObjName(m_playerCharPath + "/" + m_playerCharacters[m_characterToEdit].name + ".asset", m_editCharName);

        //Updates characters properties files
        m_playerCharacters[m_characterToEdit].discription = m_editCharDisc;
        m_playerCharacters[m_characterToEdit].level = m_editCharLevel;
        m_playerCharacters[m_characterToEdit].maxLevel = m_editCharMaxLevel;
        m_playerCharacters[m_characterToEdit].experanceCurve = m_editCharExpCurve;

        //Saves the scriptable object
        m_scriptObjUtill.SaveAssets();
    }

    private void SaveHostileObj()
    {
        //Renames the file
        m_scriptObjUtill.ChangeObjName(m_HostileCharPath + "/" + m_hostileCharacters[m_characterToEdit].name + ".asset", m_editCharName);

        //Updates characters properties files
        m_hostileCharacters[m_characterToEdit].discription = m_editCharDisc;
        m_hostileCharacters[m_characterToEdit].level = m_editCharLevel;
        m_hostileCharacters[m_characterToEdit].aggroRange = m_editAggroRange;

        //Saves the scriptable object
        m_scriptObjUtill.SaveAssets();
    }

    private void DeleteCharacter(string path)
    {
        m_scriptObjUtill.DeleteScriptableObj(path);
    }
}