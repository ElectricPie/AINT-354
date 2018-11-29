using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterTab : Tab
{
    //Player character 
    private Vector2 m_playerScrollPos;

    //Hostile character 
    private Vector2 m_hostileScrollPos;

    //General
    private int m_characterListTab = 0;
    private int m_newCharStartingLevel;

    private CharacterList m_characters;

    private Rect m_mainBoxRect;
    private Rect m_playerScrollRect;
    private Rect m_propertiesBoxRect;

    private string m_newCharName;
    private string m_newCharDisc;

    public CharacterTab()
    {
        m_tabName = "Characters";

        m_characters = new CharacterList();
    }

    public override void DisplayTab()
    {
        //Updates the dimensions for the main box in relations to the window
        m_mainBoxRect = new Rect(20, 30, m_windowSize.width - 40, m_windowSize.height - 50);
        //Draws the main box and sets the width and height to adapt to the window changing size
        GUILayout.BeginArea(m_mainBoxRect);

        float scrollHeight = m_mainBoxRect.height / 2 - 30;

        //Draws the UI
        //Changes which list of characters is displayed.
        m_characterListTab = GUI.Toolbar(new Rect(0, 0, 200, 20), m_characterListTab, new string[] { "Player", "Hostile" });
        if (m_characterListTab == 0)
        {
            DrawCharacterList(m_characters.GetPlayersAsArray());
        }
        else
        {
            DrawCharacterList(m_characters.GetHostilesAsArray());
        }
        //Draws the properties box
        DrawPropertiesBox();

        //Ends the main box
        GUILayout.EndArea();
    }

    private void DrawCharacterList(CharacterTemplate[] characters)
    {
        //Updates the dimensions for the player scroll view
        m_playerScrollRect = new Rect(0, 22, 200, m_mainBoxRect.height - 22);
        //Draws the player characters list
        m_playerScrollPos = GUI.BeginScrollView(m_playerScrollRect, m_playerScrollPos, new Rect(0, 0, m_playerScrollRect.width - 20, m_characters.GetPlayerCount() * 20));

            
        }

        //Debug buttons to check scroll view size
        for (int i = 0; i < m_characters.GetPlayerCount(); i++)
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

        //Ends the properties box
        GUILayout.EndArea();
    }

    private void DrawGeneralProperties()
    {
        //Sets the width that all tags will be
        float tagLength = 70;
        //Sets the height that all properties will be
        float propertyHeight = 20;
        //Sets the distance between each propertys 
        float propertyGap = 21;
        //Sets the width of the fields which change with the window width
        float fieldWidth = m_propertiesBoxRect.width - tagLength;

        //Name lable and field
        GUI.Label(new Rect(0, propertyGap * 0, tagLength, propertyHeight), "Name");
        m_newCharName = GUI.TextField(new Rect(tagLength, propertyGap * 0, fieldWidth, propertyHeight), m_newCharName);

        //Discription lable and field
        GUI.Label(new Rect(0, propertyGap * 1, tagLength, propertyHeight), "Discription");
        m_newCharDisc = GUI.TextArea(new Rect(tagLength, propertyGap * 1, fieldWidth, 60), m_newCharDisc);

        //Starting level lable and field
        GUI.Label(new Rect(0, propertyGap * 4, tagLength, propertyHeight), "Discription");
        m_newCharStartingLevel = EditorGUI.IntField(new Rect(tagLength, propertyGap * 4, fieldWidth, propertyHeight), m_newCharStartingLevel);
    }
}
