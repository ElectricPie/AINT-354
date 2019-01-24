using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {
    [SerializeField]
    private string m_name;
    [SerializeField]
    private float m_speed = 8;
    [SerializeField]
    private List<Attribute> m_attributes;
    private int m_level;

    private bool m_alive;

    public string Name
    {
        get{ return m_name; }
        set { m_name = value; }
    }

    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

    public List<Attribute> Attributes
    {
        get { return m_attributes; }
        set { m_attributes = value; }
    }

    public int Level
    {
        get { return m_level; }
        set { m_level = value; }
    }

    public bool Alive
    {
        get { return m_alive; }
    }
}
