using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private InputHandler m_inputHandler;

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
}
