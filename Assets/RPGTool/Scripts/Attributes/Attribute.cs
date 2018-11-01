using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute  {
    private string m_name;
    private string m_sName;
    private string m_disc;
    private int m_baseValue;
    private int m_value;

    public Attribute(string name, string sName, string disc, int baseValue)
    {
        m_name = name;
        m_sName = sName;
        m_disc = disc;
        m_baseValue = baseValue;
        m_value = m_baseValue;
    }

    public string Name
    {
        get { return m_name; }

        set { m_name = value; }
    }

    public string ShortName
    {
        get { return m_sName; }

        set { m_sName = value; }
    }

    public string Disc
    {
        get { return m_disc; }

        set { m_disc = value; }
    }

    public int BaseValue
    {
        get { return m_baseValue; }

        set { m_baseValue = value; }
    }
}
