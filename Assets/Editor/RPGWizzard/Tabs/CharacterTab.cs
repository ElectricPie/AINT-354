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
    private int numberOfChar = 20;

    public CharacterTab()
    {
        m_tabName = "Characters";
    }

    public override void DisplayTab()
    {
        //Creates the dimensions for the main box
        Rect mainBoxRect = new Rect(20, 30, m_windowSize.width - 40, m_windowSize.height - 50);
        //Draws the main box and sets the width and height to adapt to the window changing size
        GUILayout.BeginArea(mainBoxRect);

        float scrollHeight = mainBoxRect.height / 2 - 30;

        //Draws the player characters label
        GUI.Label(new Rect(0, 0, 200, 20), "Player Characters");
        //Creats the dimensions for the player scroll view
        Rect playerScrollRect = new Rect(0, 20, 200, scrollHeight);
        //Draws the player characters list
        m_playerScrollPos = GUI.BeginScrollView(playerScrollRect, m_playerScrollPos, new Rect(0, 0, playerScrollRect.width - 20, numberOfChar * 20));

        //Debug buttons to check scroll view size
        for (int i = 0; i < numberOfChar; i++)
        {
            //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
            if (GUI.Button(new Rect(0, i * 20, playerScrollRect.width - 20, 20), "New Player [" + i + "]"))
            {

            }
        }

        //Ends the player scroll view
        GUI.EndScrollView();


        //Draws the player characters label
        GUI.Label(new Rect(0, mainBoxRect.height / 2 + 10, 200, 20), "Hostile Characters");

        Rect hostileScrollRect = new Rect(0, mainBoxRect.height / 2 + 30, 200, scrollHeight);
        //Draws the player characters list
        m_hostileScrollPos = GUI.BeginScrollView(hostileScrollRect, m_hostileScrollPos, new Rect(0, 0, hostileScrollRect.width - 20, numberOfChar * 20));

        //Debug buttons to check scroll view size
        for (int i = 0; i < numberOfChar; i++)
        {
            //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
            if (GUI.Button(new Rect(0, i * 20, hostileScrollRect.width - 20, 20), "New Hostile [" + i + "]"))
            {

            }
        }

        //Ends the player scroll view
        GUI.EndScrollView();

        //Creates the dimensions for the characters properties box
        Rect propertiesBoxRect = new Rect(210, 0, mainBoxRect.width - 220, mainBoxRect.height);
        //Drawns the properties of the character 
        GUILayout.BeginArea(propertiesBoxRect);

        //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
        for (int i = 0; i < 100; i++)
        {
            if (GUI.Button(new Rect(0, i * 20, propertiesBoxRect.width, 20) , "Property"))
            {

            }
        }

        //Ends the properties box
        GUILayout.EndArea();

        //Ends the main box
        GUILayout.EndArea();
    }
}
