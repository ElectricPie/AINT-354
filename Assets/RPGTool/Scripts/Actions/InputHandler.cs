﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler {
    private ICommand m_keyboardMove;
    private ICommand m_cameraZoomIn;
    private ICommand m_cameraZoomOut;

    public InputHandler(GameObject character) {
        //Instantiates commands
        m_keyboardMove = new KeyboardMovementCommands(character);
        m_cameraZoomIn = new CameraZoomIn(character);
        m_cameraZoomOut = new CameraZoomOut(character);
    }

    public void HandleInput()
    {
        m_keyboardMove.Execute();

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            m_cameraZoomIn.Execute();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            m_cameraZoomOut.Execute();
        }
    }
}
