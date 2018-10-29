using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : CameraControls, ICommand
{
    public CameraZoomIn(GameObject character) : base(character)
    {
    }

    public void Execute()
    {
        //Prevents the camera from zooming in to far
        if (GetCameraDistance() > 5)
        {
            //Moves the camera towards the character
            m_camera.transform.localPosition += (Vector3.forward + Vector3.down);
        }
    }

    public GameObject Obj { get; set; }
}

public class CameraZoomOut : CameraControls, ICommand
{
    public CameraZoomOut(GameObject character) : base(character)
    {
    }

    public void Execute()
    {
        //Prevents camera from zooming out too far
        if (GetCameraDistance() < 15)
        {
            //Moves the camera away from the character
            m_camera.transform.localPosition += (Vector3.back + Vector3.up);
        }
    }

    public GameObject Obj { get; set; }
}

public class CameraControls
{
    private GameObject m_character;

    protected GameObject m_camera;

    public CameraControls(GameObject character)
    {
        m_character = character;
        m_camera = m_character.transform.GetChild(0).gameObject;
    }

    protected float GetCameraDistance()
    {
        //Finds the distance of the camera from the character
        return Vector3.Distance(m_camera.transform.position, m_character.transform.position);
    }
}