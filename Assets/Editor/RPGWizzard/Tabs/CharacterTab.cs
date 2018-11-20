using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterTab : Tab
{
    //Player character 
    private Vector2 m_playerScrollPos;

    public CharacterTab()
    {
        m_tabName = "Characters";
    }

    public override void DisplayTab()
    {
        GUILayout.Label("Player Character", EditorStyles.boldLabel);

        //Creates the dimensions for the main box
        Rect mainBoxRect = new Rect(20, 50, m_windowSize.width - 40, m_windowSize.height - 70);
        //Draws the main box and sets the width and height to adapt to the window changing size
        GUILayout.BeginArea(mainBoxRect);

        //Creats the dimensions for the player scroll view
        Rect playerScrollRect = new Rect(0, 0, 200, mainBoxRect.height / 2);
        //Draws the player characters list
        m_playerScrollPos = GUI.BeginScrollView(playerScrollRect, m_playerScrollPos, new Rect(0, 0, playerScrollRect.width - 20, 2000));


        //Debug buttons to check scroll view size
        for (int i = 0; i < 100; i++)
        {
            //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
            if (GUI.Button(new Rect(0, i * 20, playerScrollRect.width - 20, 20), "New Player"))
            {

            }
        }

        //Ends the player scroll view
        GUI.EndScrollView();

        //Ends the main box
        GUILayout.EndArea();
    }
}
