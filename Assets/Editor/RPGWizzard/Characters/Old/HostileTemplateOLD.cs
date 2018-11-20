using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileTemplateOLD : CharacterTemplate {
    private float m_aggroRange;

    public float AggroRange
    {
        get { return m_aggroRange; }
        set { m_aggroRange = value; }
    }
}
