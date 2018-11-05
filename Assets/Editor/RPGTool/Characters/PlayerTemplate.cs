using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerTemplate : CharacterTemplate
{
    private int m_minLevel;
    private int m_maxLevel;

    private AnimationCurve m_experienceCurve;

    public AnimationCurve ExperianceCurve
    {
        get { return m_experienceCurve; }
        set { m_experienceCurve = value; }
    }

    public int MinLevel
    {
        get { return m_minLevel; }
        set { m_minLevel = value; }
    }

    public int MaxLevel
    {
        get { return m_maxLevel; }
        set { m_maxLevel = value; }
    }
}
