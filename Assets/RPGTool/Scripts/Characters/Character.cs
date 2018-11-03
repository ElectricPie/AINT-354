using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    private string m_name;
    private int m_health;
    private int m_resources;

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

    public bool Alive
    {
        get { return m_alive; }
    }
}
