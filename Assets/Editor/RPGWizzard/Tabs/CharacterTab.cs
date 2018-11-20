using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterTab : Tab {

    public CharacterTab()
    {
        m_tabName = "Characters";
    }

    public override void DisplayTab()
    {
        PlayerCharacterList();
    }

    private void PlayerCharacterList()
    {
        GUILayout.Label("Player Character", EditorStyles.boldLabel);

        Debug.Log("Window Size: " + m_windowSize);

        //Draws the main box and sets the width and height to adapt to the window changing size
        GUILayout.BeginArea(new Rect(20, 50, m_windowSize.width - 40, m_windowSize.height - 70));

        //Debug buttons to check area size
        for (int i = 0; i < 100; i++)
        {
            if (GUILayout.Button("New Player"))
            {

            }
        }

        //Ends the main box
        GUILayout.EndArea();
    }
}
