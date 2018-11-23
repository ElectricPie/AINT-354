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
    public int characterListTab = 0;
    private int m_numberOfChar = 20;

    private CharacterList m_characters;

    public CharacterTab()
    {
        m_tabName = "Characters";

        m_characters = new CharacterList();
    }

    public override void DisplayTab()
    {
        //Creates the dimensions for the main box
        Rect mainBoxRect = new Rect(20, 30, m_windowSize.width - 40, m_windowSize.height - 50);
        //Draws the main box and sets the width and height to adapt to the window changing size
        GUILayout.BeginArea(mainBoxRect);

        float scrollHeight = mainBoxRect.height / 2 - 30;

        //Draws the UI
        //Changes which list of characters is displayed.
        characterListTab = GUI.Toolbar(new Rect(0, 0, 200, 20), characterListTab, new string[] { "Player", "Hostile" });
        if (characterListTab == 0)
        {
            DrawCharacterList(mainBoxRect, m_characters.GetPlayersAsArray());
        }
        else
        {
            DrawCharacterList(mainBoxRect, m_characters.GetHostilesAsArray());
        }
        //Draws the properties box
        DrawPropertiesBox(mainBoxRect);

        //Ends the main box
        GUILayout.EndArea();
    }

    private void DrawCharacterList(Rect mainBoxRect, CharacterTemplate[] characters)
    {
        //Creats the dimensions for the player scroll view
        Rect playerScrollRect = new Rect(0, 22, 200, mainBoxRect.height - 22);
        //Draws the player characters list
        m_playerScrollPos = GUI.BeginScrollView(playerScrollRect, m_playerScrollPos, new Rect(0, 0, playerScrollRect.width - 20, m_characters.GetPlayerCount() * 20));

        if (GUI.Button(new Rect(0, 0, playerScrollRect.width - 20, 20), "New Character")) {
            
        }

        //Debug buttons to check scroll view size
        for (int i = 0; i < m_characters.GetPlayerCount(); i++)
        {
            //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
            if (GUI.Button(new Rect(0, i * 20 + 21, playerScrollRect.width - 20, 20), "Player [" + i + "]"))
            {

            }
        }

        GUI.EndScrollView();
    }

    private void DrawPropertiesBox(Rect mainBoxRect)
    {
        //Creates the dimensions for the characters properties box
        Rect propertiesBoxRect = new Rect(210, 0, mainBoxRect.width - 220, mainBoxRect.height);
        //Drawns the properties of the character 
        GUILayout.BeginArea(propertiesBoxRect);

        //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
        for (int i = 0; i < 100; i++)
        {
            if (GUI.Button(new Rect(0, i * 20, propertiesBoxRect.width, 20), "Property"))
            {

            }
        }

        //Ends the properties box
        GUILayout.EndArea();
    }
}
