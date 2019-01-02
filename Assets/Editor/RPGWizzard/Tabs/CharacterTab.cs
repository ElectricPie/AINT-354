using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterTab : Tab
{
    //Player character 
    private Vector2 m_playerScrollPos;

    private int m_newPlayerMaxLevel;

    private List<ScriptablePlayer> m_playerCharacters;

    //Hostile character 
    private Vector2 m_hostileScrollPos;

    //General
    private int m_characterListTab = 0;
    private int m_newCharStartingLevel;

    private ScriptableObjUtil ScriptObjUtill;

    private Rect m_mainBoxRect;
    private Rect m_playerScrollRect;
    private Rect m_propertiesBoxRect;

    private string m_newCharName;
    private string m_newCharDisc;

    private Vector2 m_attributeListScrollPos;
    private List<Attribute> m_newCharAttributes;

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

        ScriptObjUtill = new ScriptableObjUtil();

        m_playerCharacters = new List<ScriptablePlayer>();
    }

    public override void DisplayTab()
    {
        //Updates the dimensions of the drawing space in relations to the window
        m_mainBoxRect = new Rect(20, 30, m_windowSize.width - 40, m_windowSize.height - 50);
        //Creats the main box using the adaptable rect
        GUILayout.BeginArea(m_mainBoxRect);
        //Updates the width of the fields which change with the main boxs width
        m_fieldWidth = m_propertiesBoxRect.width - m_tagLength;

        //Calculates the height of the character scroll height so that it fits within and adapts to the main box
        float scrollHeight = m_mainBoxRect.height / 2 - 30;

        //Draws the UI
        //Creates a tool bar which is used to select what type of characters the list should show
        m_characterListTab = GUI.Toolbar(new Rect(0, 0, 200, 20), m_characterListTab, new string[] { "Player", "Hostile" });

        //Draws the player/hostile list depending on the toolbar selection
        if (m_characterListTab == 0)
        {

            //Tempareraly here till new character is properly created
            DrawCharacterList(GetCharacterNames(m_playerCharacters));
        }
        else
        {
            
        }
        //Draws the properties box
        DrawPropertiesBox();

        //Ends the main box
        GUILayout.EndArea();
    }

    private List<string> GetCharacterNames(List<ScriptablePlayer> characters)
    {
        List<string> names = new List<string>();

        //Creates a list of names from the characters
        for (int i = 0; i < characters.Count; i++)
        {
            names[i] = characters[i].name;
        }

        return names;
    }

    private void DrawCharacterList(List<string> names)
    {
        //Updates the dimensions for the player scroll view
        m_playerScrollRect = new Rect(0, 22, 200, m_mainBoxRect.height - 22);
        //Draws the player characters list
        m_playerScrollPos = GUI.BeginScrollView(m_playerScrollRect, m_playerScrollPos, new Rect(0, 0, m_playerScrollRect.width - 20, names.Count * 20));

        if (GUI.Button(new Rect(0, 0, m_playerScrollRect.width - 20, 20), "New Character")) {
            
        }

        //Debug buttons to check scroll view size
        for (int i = 0; i < names.Count; i++)
        {
            //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
            if (GUI.Button(new Rect(0, i * 20 + 21, m_playerScrollRect.width - 20, 20), "Player [" + i + "]"))
            {

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

        DrawGeneralProperties();

        if (m_characterListTab == 0)
        {
            DrawPlayerProperties();
            DrawAttributesList(130);


            if(GUI.Button(new Rect(0, m_propertyGap * 6, m_tagLength, m_propertyGap), "Content"))
            {
                //Creates a scriptable object
                ScriptablePlayer newPlayerChar = ScriptableObject.CreateInstance<ScriptablePlayer>();
                //Assigns values to the new object 
                newPlayerChar.name = m_newCharName;
                newPlayerChar.discription = m_newCharDisc;
                newPlayerChar.level = m_newCharStartingLevel;
                newPlayerChar.maxLevel = m_newPlayerMaxLevel;

                //Creates the new character as a scriptable object 
                ScriptObjUtill.CreateNewScriptableObj(newPlayerChar, m_newCharName, "Assets/RPGWizzard/Characters/Players/");
            }
        }
        else
        {

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
    }

    private void DrawPlayerProperties()
    {
        //Starting level lable and field
        GUI.Label(new Rect(0, m_propertyGap * 4, m_tagLength, m_propertyHeight), "Starting Level");
        m_newCharStartingLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 4, m_fieldWidth, m_propertyHeight), m_newCharStartingLevel);

        //Max Level lable and field
        GUI.Label(new Rect(0, m_propertyGap * 5, m_tagLength, m_propertyHeight), "Max Level");
        m_newPlayerMaxLevel = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 5, m_fieldWidth, m_propertyHeight), m_newPlayerMaxLevel);
    }

    private void DrawAttributesList(int yStart)
    {
        //Updates the dimensions for the attribute scroll view
        Rect attributeScrollRect = new Rect(0, yStart, 200, m_mainBoxRect.height - 22);
        //Draws the attribute list
        m_attributeListScrollPos = GUI.BeginScrollView(attributeScrollRect, m_attributeListScrollPos, attributeScrollRect);

        GUI.EndScrollView();
    }
}