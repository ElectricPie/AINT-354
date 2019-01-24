using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private InputHandler m_inputHandler;
    private AnimationCurve m_experianceCurve;
    private int m_maxLevel;
    private int m_experiance;

    // Use this for initialization
    void Start()
    {
        //Command design pattern
        m_inputHandler = new InputHandler(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Calls the input handler
        m_inputHandler.HandleInput();
    }

    public int MaxLevel
    {
        get { return m_maxLevel; }
        set { m_maxLevel = value; }
    }

    public AnimationCurve ExperianceCurve
    {
        get { return m_experianceCurve; }
        set { m_experianceCurve = value; }
    }
}
