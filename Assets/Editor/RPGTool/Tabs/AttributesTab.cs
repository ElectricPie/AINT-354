using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttributesTab : Tab
{
    private Vector2 m_scrollPosition;

    private AttributesList m_attributesList;
     
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

    private SaveLoadAttributes m_attributeStore;

    public AttributesTab()
    {
        m_tabName = "Attributes";
        m_attributeStore = new SaveLoadAttributes();

        m_attributesList = m_attributeStore.Load();
         
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
                for (int i = 0; i < m_attributesList.GetAttributes().Count; i++)
                {
                    if (GUILayout.Button(m_attributesList.GetAttributes()[i].Name))
                    {
                        m_attributeToEdit = i;
                        UpdateEditDisplay();
                    }
                }

                //Ends the scroll view
                GUILayout.EndScrollView();

                //Starts displaying the edit attribute section
                if (m_attributesList.GetAttributes().Count > 0)
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

            //Check if an attribute with the same name exists already
            if (m_attributesList.CheckIfAttribute(newAttribute) == -1)
            {
                m_attributesList.AddAttribute(newAttribute);

                m_attributeStore.Save(m_attributesList);
                 
                ResetNewAttributesTextField();
            }
            else
            {
                Debug.LogError("An attribute with the same name already exists");
            }
        }

        //Draws the cancel button
        if (GUILayout.Button("Cancel"))
        {
            ResetNewAttributesTextField();
        }
    }

    private void ResetNewAttributesTextField()
    {
        //Sets the tabs state back to the scroll view
        m_tabState = 0;

        //Resets the text fields to blank
        m_newAttributeName = "";
        m_newAttributeSName = "";
        m_newAttributeDisc = "";
        m_newAttributeBaseValue = 0;

        //Pulls foucs away allowing values to reset
        GUI.FocusControl("");
    }

    private void UpdateEditDisplay()
    {
        List<Attribute> attributeList = m_attributesList.GetAttributes();

        //Updates the edit section with the needed attributes data
        m_editAttributeName = attributeList[m_attributeToEdit].Name;
        m_editAttributeSName = attributeList[m_attributeToEdit].ShortName;
        m_editAttributeDisc = attributeList[m_attributeToEdit].Disc;
        m_editAttributeBaseValue = attributeList[m_attributeToEdit].BaseValue;

        //Pulls foucs away allowing values to reset
        GUI.FocusControl("");
    }

    private void EditAttribute()
    {
        GUILayout.Label("Edit Attribute", EditorStyles.boldLabel);

        //Draws the field for getting edited attribute data
        m_editAttributeName = EditorGUILayout.TextField("Name", m_editAttributeName);
        m_editAttributeSName = EditorGUILayout.TextField("Short Name", m_newAttributeSName);
        m_editAttributeDisc = EditorGUILayout.TextField("Discription", m_newAttributeDisc);
        m_editAttributeBaseValue = EditorGUILayout.IntField("Base Value", m_newAttributeBaseValue);

        //Draws the button for editing attributes
        if (GUILayout.Button("Edit Attribute"))
        {
            //Sets the new edited values
            m_attributesList.ChangeAttributeName(m_attributeToEdit, m_editAttributeName);
            m_attributesList.ChangeAttributeSName(m_attributeToEdit, m_editAttributeSName);
            m_attributesList.ChangeAttributeDisc(m_attributeToEdit, m_editAttributeDisc);
            m_attributesList.ChangeAttributeBaseValue(m_attributeToEdit, m_editAttributeBaseValue);

            m_attributeStore.Save(m_attributesList);
        }

        //Draws the delete attribute button
        if (GUILayout.Button("Delete Attribute"))
        {
            m_attributesList.RemoveAttribute(m_attributeToEdit);

            m_attributeStore.Save(m_attributesList);
        }
    }
}
