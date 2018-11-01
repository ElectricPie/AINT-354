using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttributesTab : Tab
{
    private Vector2 m_scrollPosition;

    private List<Attribute> m_attributes;

    private string m_newAttributeName;
    private string m_newAttributeSName;
    private string m_newAttributeDisc;
    private int m_newAttributeBaseValue;

    private int m_tabState = 0;

    public AttributesTab()
    {
        m_tabName = "Attributes";
        m_attributes = new List<Attribute>();
    }

    public override void DisplayTab()
    {
        switch (m_tabState)
        {
            case 0:


                GUILayout.Label("Attributes", EditorStyles.boldLabel);

                //Draws the scroll view
                m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition, GUILayout.Width(200), GUILayout.Height(100));

                if (GUILayout.Button("New Attribute"))
                {
                    m_tabState = 1;
                }

                for (int i = 0; i < m_attributes.Count; i++)
                {
                    Debug.Log(m_attributes[i].Name);
                    GUILayout.Label(m_attributes[i].Name);
                }

                //Ends the scroll view
                GUILayout.EndScrollView();
                break;
            case 1:
                NewAttribute();
                break;
        }

        
    }

    //Tab state 1
    private void NewAttribute()
    {
        GUILayout.Label("New Attribute", EditorStyles.boldLabel);

        //Draws and gets the values for a new attribute
        m_newAttributeName = EditorGUILayout.TextField("Name", m_newAttributeName);
        m_newAttributeSName = EditorGUILayout.TextField("Short Name", m_newAttributeSName);
        m_newAttributeDisc = EditorGUILayout.TextField("Discription", m_newAttributeDisc);
        m_newAttributeBaseValue = EditorGUILayout.IntField("Base Value", m_newAttributeBaseValue);

        if (GUILayout.Button("Create New Attribute"))
        {
            //Creates a new attribute
            Attribute newAttribute = new Attribute
            {
                Name = m_newAttributeName,
                ShortName = m_newAttributeSName,
                Disc = m_newAttributeDisc,
                BaseValue = m_newAttributeBaseValue
            };

            //Adds the attribute to the list of attributes
            m_attributes.Add(newAttribute);

            m_tabState = 0;
        }

        if (GUILayout.Button("Cancel"))
        {
            m_tabState = 0;
        }
    }
}
