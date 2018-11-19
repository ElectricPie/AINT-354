using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesList
{
    private List<Attribute> m_attributes;

    public AttributesList()
    {
        m_attributes = new List<Attribute>();
    }

    public void AddAttribute(Attribute attributeToAdd)
    {
        m_attributes.Add(attributeToAdd);
    }

    public void RemoveAttribute(int index)
    {
        m_attributes.RemoveAt(index);
    }

    public int CheckIfAttribute(Attribute attribute)
    {
        for (int i = 0; i < m_attributes.Count; i++)
        {
            //Checks if an attribute with the same name exists already
            if (m_attributes[i].Name == attribute.Name)
            {
                return i;
            }
        }

        return -1;
    }

    public List<Attribute> GetAttributes()
    {
        return m_attributes;
    }

    public void ChangeAttributeName(int index, string newName)
    {
        m_attributes[index].Name = newName;
    }

    public void ChangeAttributeSName(int index, string newSName)
    {
        m_attributes[index].ShortName = newSName;
    }

    public void ChangeAttributeDisc(int index, string newDisc)
    {
        m_attributes[index].Disc = newDisc;
    }

    public void ChangeAttributeBaseValue(int index, int newBase)
    {
        m_attributes[index].BaseValue = newBase;
    }
}
