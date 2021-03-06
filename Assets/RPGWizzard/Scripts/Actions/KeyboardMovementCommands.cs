﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovementCommands : ICommand
{
    private Rigidbody m_rigidbody;

    public KeyboardMovementCommands(GameObject obj)
    {
        Obj = obj;
        m_rigidbody = Obj.GetComponent<Rigidbody>();
    }

    public void Execute()
    {
        //Get the speed from the 
        float speed = Obj.GetComponent<Character>().Speed;

        //Moves the charater based on the axis inputs
        m_rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
    }

    public GameObject Obj { get; set; }
}

