using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {
    [SerializeField]
    private string m_name;
    private int m_health;
    private int m_resources;
    [SerializeField]
    private float m_speed = 8;
    private int m_level;

    private bool m_alive;


    public void TakeDamage(int damage)
    {
        //Removes the damage from the characters health
        m_health -= damage;

        UpdateIfAlive();
    }

    private void UpdateIfAlive()
    {
        //Checks if the characters health is below the death threshold
        if (m_health <= 0)
        {
            m_alive = false;
        }
        else
        {
            m_alive = true;
        }
    }

    public string Name
    {
        get{ return m_name; }
        set { m_name = value; }
    }

    public int Heath
    {
        get { return m_health; }
    }

    public int Resources
    {
        get { return m_resources; }
    }

    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

    public float Level
    {
        get { return m_level; }
        set { m_level = value; }
    }

    public bool Alive
    {
        get { return m_alive; }
    }
}
