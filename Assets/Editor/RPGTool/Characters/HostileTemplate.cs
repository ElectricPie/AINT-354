using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileTemplate : CharacterTemplate {
    private float m_aggroRange;

    public float AggroRange
    {
        get { return m_aggroRange; }
        set { m_aggroRange = value; }
    }
}
