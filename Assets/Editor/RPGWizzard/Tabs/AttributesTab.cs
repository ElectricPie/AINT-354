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

    private Rect m_mainBoxRect;
    private Rect m_propertiesBoxRect;
    private Rect m_attributeScrollRect;
    private Vector2 m_attributeScrollPos;

    //Sets the width that all tags will be
    private float m_tagLength = 95;
    //Sets the height that all properties will be
    private float m_propertyHeight = 20;
    //Sets the distance between each propertys 
    private float m_propertyGap = 21;
    private float m_fieldWidth;

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
        //Updates the dimensions of the drawing space in relations to the window
        m_mainBoxRect = new Rect(20, 30, m_windowSize.width - 40, m_windowSize.height - 50);
        //Creats the main box using the adaptable rect
        GUILayout.BeginArea(m_mainBoxRect);
        //Updates the width of the fields which change with the main boxs width
        m_fieldWidth = m_propertiesBoxRect.width - m_tagLength;

        DrawAttributesList();

        DrawPropertiesBox();

        GUILayout.EndArea();
    }

    private void DrawPropertiesBox()
    {
        //Creates the dimensions for the attributes properties box
        m_propertiesBoxRect = new Rect(210, 0, m_mainBoxRect.width - 220, m_mainBoxRect.height);
        //Drawns the properties of the attributes 
        GUILayout.BeginArea(m_propertiesBoxRect);

        switch (m_tabState)
        {
            case 0:
                //Displays the new attribute section
                NewAttribute();
                break;
            case 1:
                //Displays the edit attribute section
                EditAttribute();
                break;
        }

        //Ends the properties box
        GUILayout.EndArea();
    }

    private void DrawAttributesList()
    {
        //Updates the dimensions for the player scroll view
        m_attributeScrollRect = new Rect(0, 22, 200, m_mainBoxRect.height - 20);
        //Draws the player characters list
        m_attributeScrollPos = GUI.BeginScrollView(m_attributeScrollRect, m_attributeScrollPos, new Rect(0, 0, m_attributeScrollRect.width - 20, (m_attributes.Count + 1) * 20));

        if (GUI.Button(new Rect(0, 0, m_attributeScrollRect.width - 20, 20), "New Attribute"))
        {
            m_tabState = 0;
        }

        //Debug buttons to check scroll view size
        for (int i = 0; i < m_attributes.Count; i++)
        {
            //Creates a buttons with a interval of 20 between each button and with a width 20 less than the scroll view to allow for the scroll bar.
            if (GUI.Button(new Rect(0, i * 20 + 21, m_attributeScrollRect.width - 20, 20), m_attributes[i].name))
            {
                m_attributeToEdit = i;
                UpdateEditDisplay();
            }
        }

        GUI.EndScrollView();
    }

    //Tab state 1
    private void NewAttribute()
    {
        GUI.Label(new Rect(0, m_propertyGap * 0, m_tagLength, m_propertyHeight), "New Attribute", EditorStyles.boldLabel);

        //Draws and gets the values for a new attribute
        GUI.Label(new Rect(0, m_propertyGap * 1, m_tagLength, m_propertyHeight), "Name");
        m_newAttributeName = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 1, m_fieldWidth, m_propertyHeight), m_newAttributeName);

        GUI.Label(new Rect(0, m_propertyGap * 2, m_tagLength, m_propertyHeight), "Short Name");
        m_newAttributeSName = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 2, m_fieldWidth, m_propertyHeight), m_newAttributeSName);

        GUI.Label(new Rect(0, m_propertyGap * 3, m_tagLength, m_propertyHeight), "Description");
        m_newAttributeDisc = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 3, m_fieldWidth, m_propertyHeight), m_newAttributeDisc);

        GUI.Label(new Rect(0, m_propertyGap * 4, m_tagLength, m_propertyHeight), "Base Value");
        m_newAttributeBaseValue = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 4, m_fieldWidth, m_propertyHeight), m_newAttributeBaseValue);


        if (GUI.Button(new Rect(0, m_propertyGap * 5, m_propertiesBoxRect.width, m_propertyGap), "Create New Attribute"))
        {
            //Creates the attribute
            CreateNewAttribute();

            //Re-gets the attributs list so new attribute is included
            GetAttributes();

            //Removes the new attributes fields
            ResetNewAttributesTextField();
        }

        //Draws the cancel button
        if (GUI.Button(new Rect(0, m_propertyGap * 6, m_propertiesBoxRect.width, m_propertyGap), "Cancel"))
        {
            ResetNewAttributesTextField();
        }
    }

    private void CreateNewAttribute()
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

        m_tabState = 1;
    }

    //Tab State 0
     private void EditAttribute()
    {
        GUI.Label(new Rect(0, m_propertyGap * 0, m_tagLength, m_propertyHeight), "Edit Attribute", EditorStyles.boldLabel);

        //Draws the field for getting edited attribute data
        GUI.Label(new Rect(0, m_propertyGap * 1, m_tagLength, m_propertyHeight), "Name");
        m_editAttributeName = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 1, m_fieldWidth, m_propertyHeight), m_editAttributeName);

        GUI.Label(new Rect(0, m_propertyGap * 2, m_tagLength, m_propertyHeight), "Short Name");
        m_editAttributeSName = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 2, m_fieldWidth, m_propertyHeight), m_editAttributeSName);

        GUI.Label(new Rect(0, m_propertyGap * 3, m_tagLength, m_propertyHeight), "Discription");
        m_editAttributeDisc = GUI.TextField(new Rect(m_tagLength, m_propertyGap * 3, m_fieldWidth, m_propertyHeight), m_editAttributeDisc);

        GUI.Label(new Rect(0, m_propertyGap * 4, m_tagLength, m_propertyHeight), "BaseValue");
        m_editAttributeBaseValue = EditorGUI.IntField(new Rect(m_tagLength, m_propertyGap * 4, m_fieldWidth, m_propertyHeight), m_editAttributeBaseValue);

        if (GUI.Button(new Rect(0, m_propertyGap * 5, m_propertiesBoxRect.width, m_propertyGap), "Edit Attribute"))
        {
            //Edits the saved attribute
            EditAttributeObj();

            //Re-gets the attributs list so new attribute is included
            GetAttributes();

            //Removes the new attributes fields
            ResetNewAttributesTextField();
        }

        //Draws the cancel button
        if (GUI.Button(new Rect(0, m_propertyGap * 6, m_propertiesBoxRect.width, m_propertyGap), "Cancel"))
        {
            m_tabState = 0;

            ResetNewAttributesTextField();
        }
    }
    
    private void EditAttributeObj()
    {
        //Renames the file
        m_scriptObjUtill.ChangeObjName(m_attributesPath + "/" + m_attributes[m_attributeToEdit].name + ".asset", m_editAttributeName);

        //Updates attributes files
        m_attributes[m_attributeToEdit].sName = m_editAttributeSName;
        m_attributes[m_attributeToEdit].disc = m_editAttributeDisc;
        m_attributes[m_attributeToEdit].baseValue = m_editAttributeBaseValue;

        //Saves the scriptable object
        m_scriptObjUtill.SaveAssets();
    }

    private void GetAttributes()
    {
        //Gets a list of the attribues from the files
        m_attributes = m_scriptObjUtill.GetScriptableObjs<ScriptableAttribute>(m_attributesPath);
    }
}
