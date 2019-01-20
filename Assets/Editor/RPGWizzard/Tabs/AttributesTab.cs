using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttributesTab : Tab
{
    private Vector2 m_scrollPosition;

    private string m_newAttributeName;
    private string m_newAttributeSName;
    private string m_newAttributeDisc;
    private int m_newAttributeBaseValue;

    private string m_editAttributeName;
    private string m_editAttributeSName;
    private string m_editAttributeDisc;
    private int m_editAttributeBaseValue;

    private string m_attributesPath = "Assets/RPGWizzard/Attributes";

    private int m_tabState = 0;

    private int m_attributeToEdit;

    private List<ScriptableAttribute> m_attributes;

    private ScriptableObjUtil m_scriptObjUtill;

    public AttributesTab()
    {
        m_tabName = "Attributes";       
        m_attributeToEdit = 0;

        m_scriptObjUtill = new ScriptableObjUtil();

        //Gets the inital list of attributes
        GetAttributes();
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
                    if (GUILayout.Button(m_attributes[i].name))
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
            //Creates a player character scriptable object
            ScriptableAttribute newAttribute = ScriptableObject.CreateInstance<ScriptableAttribute>();

            //Assigns values to the new player character object 
            newAttribute.name = m_newAttributeName;
            newAttribute.sName = m_newAttributeSName;
            newAttribute.disc = m_newAttributeDisc;
            newAttribute.baseValue = m_newAttributeBaseValue;

            //Creates the new character as a scriptable object 
            m_scriptObjUtill.CreateNewScriptableObj(newAttribute, m_newAttributeName, "Assets/RPGWizzard/Attributes/");

            //Re-gets the attributs list so new attribute is included
            GetAttributes();

            //Removes the new attributes fields
            ResetNewAttributesTextField();
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
        //Updates the edit section with the needed attributes data
        m_editAttributeName = m_attributes[m_attributeToEdit].name;
        m_editAttributeSName = m_attributes[m_attributeToEdit].sName;
        m_editAttributeDisc = m_attributes[m_attributeToEdit].disc;
        m_editAttributeBaseValue = m_attributes[m_attributeToEdit].baseValue;

        //Pulls foucs away allowing values to reset
        GUI.FocusControl("");
    }

    private void EditAttribute()
    {
        GUILayout.Label("Edit Attribute", EditorStyles.boldLabel);

        //Draws the field for getting edited attribute data
        m_editAttributeName = EditorGUILayout.TextField("Name", m_editAttributeName);
        m_editAttributeSName = EditorGUILayout.TextField("Short Name", m_editAttributeSName);
        m_editAttributeDisc = EditorGUILayout.TextField("Discription", m_editAttributeDisc);
        m_editAttributeBaseValue = EditorGUILayout.IntField("Base Value", m_editAttributeBaseValue);

        //Draws the button for editing attributes
        if (GUILayout.Button("Update Attribute"))
        {
            //Renames the file
            m_scriptObjUtill.ChangeObjName(m_attributesPath + "/" + m_attributes[m_attributeToEdit].name + ".asset", m_editAttributeName);

            //Updates attributes files
            m_attributes[m_attributeToEdit].sName = m_editAttributeSName;
            m_attributes[m_attributeToEdit].disc = m_editAttributeDisc;
            m_attributes[m_attributeToEdit].baseValue = m_editAttributeBaseValue;

            //Saves the scriptable object
            m_scriptObjUtill.SaveAssets();

            //Refreshes the list 
            GetAttributes();
        }

        //Draws the delete attribute button
        if (GUILayout.Button("Delete Attribute"))
        {
            //Deletes the attribute
            m_scriptObjUtill.DeleteScriptableObj(m_attributesPath + "/" + m_attributes[m_attributeToEdit].name + ".asset");

            //Refreshes the list 
            GetAttributes();
        }
    }

    private void GetAttributes()
    {
        //Gets a list of the attribues from the files
        m_attributes = m_scriptObjUtill.GetScriptableObjs<ScriptableAttribute>(m_attributesPath);
    }
}
