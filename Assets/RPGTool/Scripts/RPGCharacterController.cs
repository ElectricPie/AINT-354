using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterController : MonoBehaviour {
    public float speed = 10;

    private Rigidbody m_rigidbody;

    private GameObject m_camera;

	// Use this for initialization
	void Start () {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_camera = this.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        Camera();
	}

    private void Movement()
    {
        //Applies axis input into velocity
        m_rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
    }

    private void Camera()
    {
        //Gets the distance between the camera and the character
        float cameraDistance = Vector3.Distance(m_camera.transform.position, this.transform.position);

       //Checks whether the scroll wheel is going up or down and prevents the camera from zooming too far in or out
       if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraDistance > 5)
       {
           ZoomIn();
       }
       else if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraDistance < 15)
       {
           ZoomOut();
       } 
    }

    private void ZoomIn()
    {
        //Moves the camera toward the character
        m_camera.transform.localPosition += (Vector3.forward + Vector3.down);
    }

    private void ZoomOut()
    {
        //Moves the camera away from the character
        m_camera.transform.localPosition += (Vector3.back + Vector3.up);
    }
}
