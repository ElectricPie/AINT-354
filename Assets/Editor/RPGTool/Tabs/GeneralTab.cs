using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GeneralTab : Tab
{
    private string m_text = "";

    public override void DisplayTab()
    {
        //Creates label
        GUILayout.Label("Test GUI", EditorStyles.boldLabel);
        //Creates editable text field
        m_text = EditorGUILayout.TextField("Text Field", m_text);
    }
}
