using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterController : MonoBehaviour {
    public float speed = 10;

    private InputHandler m_inputHandler;

    private List<Attribute> m_attributes;

	// Use this for initialization
	void Start () {
        //Command design pattern
        m_inputHandler = new InputHandler(gameObject);

        m_attributes = new List<Attribute>();
    }
	
	// Update is called once per frame
	void Update () {
        //Calls the input handler
        m_inputHandler.HandleInput();

        for(int i = 0; i < m_attributes.Count; i++) {
            Debug.Log(m_attributes[i].Name);
        }
	}

    public void AttachAttribute(Attribute newAttribute)
    {
        //Checks if the attribue is already attached
        if (!CheckForAttribute(newAttribute))
        {
            //Adds the attribute
            m_attributes.Add(newAttribute);
        }
        else
        {
            Debug.LogError("Attribute already attached");
        }
    }

    public void RemoveAttribute(Attribute attributeToRemove)
    {
        //Check if the attibute is attached
        if (CheckForAttribute(attributeToRemove))
        {
            //Removes the attribute
            m_attributes.Remove(attributeToRemove);
        }
        else
        {
            Debug.LogError("Attribute is not attached");
        }
    }

    private bool CheckForAttribute(Attribute attribute)
    {
        //Checks through attached attributes
        for (int i = 0; i < m_attributes.Count; i++)
        {
            if (m_attributes[i] == attribute)
            {
                return true;
            }
        }

        return false;
    }

    public float Speed {
        get { return speed; }
    }
}
