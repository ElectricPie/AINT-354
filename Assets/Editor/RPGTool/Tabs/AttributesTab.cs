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

    private string m_editAttributeName;
    private string m_editAttributeSName;
    private string m_editAttributeDisc;
    private int m_editAttributeBaseValue;

    private int m_tabState = 0;

    private int m_attributeToEdit;

    public AttributesTab()
    {
        m_tabName = "Attributes";
        m_attributes = new List<Attribute>();
        m_attributeToEdit = 0;
    }

    public override void DisplayTab()
    {
        switch (m_tabState)
        {
            case 0:
                GUILayout.Label("Attributes", EditorStyles.boldLabel);

                //Draws the scroll view
                m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition, GUILayout.Width(200), GUILayout.Height(100));

                //Draws the new attribute button
                if (GUILayout.Button("New Attribute"))
                {
                    m_tabState = 1;
                }

                //Draws all attributes in the scroll view
                for (int i = 0; i < m_attributes.Count; i++)
                {
                    if (GUILayout.Button(m_attributes[i].Name))
                    {
                        m_attributeToEdit = i;
                        UpdateEditDisplay();
                    }
                }

                //Ends the scroll view
                GUILayout.EndScrollView();

                //Starts displaying the edit attribute section
                if (m_attributes.Count > 0)
                {
                    EditAttribute();
                }
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

            Debug.Log("Attribute: " + CheckIfAttribute(newAttribute));

            if (!CheckIfAttribute(newAttribute))
            {
                //Adds the attribute to the list of attributes
                m_attributes.Add(newAttribute);

                ResetNewAttributesTextField();
            }
            else
            {
                Debug.LogError("An attribute with the same name already exists");
            }
        }

        if (GUILayout.Button("Cancel"))
        {
            ResetNewAttributesTextField();
        }
    }

    private void ResetNewAttributesTextField()
    {
        //Sets the tabs state back to the scroll view
        m_tabState = 0;

        m_newAttributeName = "";
        m_newAttributeSName = "";
        m_newAttributeDisc = "";
        m_newAttributeBaseValue = 0;

        //Pulls foucs away allowing values to reset
        GUI.FocusControl("");
    }

    private void UpdateEditDisplay()
    {
        //Updates the edit section with the needed attributes data
        m_editAttributeName = m_attributes[m_attributeToEdit].Name;
        m_editAttributeSName = m_attributes[m_attributeToEdit].ShortName;
        m_editAttributeDisc = m_attributes[m_attributeToEdit].Disc;
        m_editAttributeBaseValue = m_attributes[m_attributeToEdit].BaseValue;
    }

    private void EditAttribute()
    {
        GUILayout.Label("Edit Attribute", EditorStyles.boldLabel);

        //Draws the field for getting edited attribute data
        m_editAttributeName = EditorGUILayout.TextField("Name", m_editAttributeName);
        m_editAttributeSName = EditorGUILayout.TextField("Short Name", m_newAttributeSName);
        m_editAttributeDisc = EditorGUILayout.TextField("Discription", m_newAttributeDisc);
        m_editAttributeBaseValue = EditorGUILayout.IntField("Base Value", m_newAttributeBaseValue);

        if (GUILayout.Button("Edit Attribute"))
        {
            //Sets the new edited values
            m_attributes[m_attributeToEdit].Name = m_editAttributeName;
            m_attributes[m_attributeToEdit].ShortName = m_editAttributeSName;
            m_attributes[m_attributeToEdit].Disc = m_editAttributeDisc;
            m_attributes[m_attributeToEdit].BaseValue = m_editAttributeBaseValue;

        }
    }

    private bool CheckIfAttribute(Attribute attribute)
    {
        for (int i = 0; i < m_attributes.Count; i++)
        {
            if (m_attributes[i].Name == attribute.Name)
            {
                return true;
            }
        }

        return false;
    }
}
