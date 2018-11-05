using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterTemplate {
    private string m_name;
    private float m_speed;
    private int m_level;

    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

    public int Level
    {
        get { return m_level; }
        set { m_level = value; }
    }
}
