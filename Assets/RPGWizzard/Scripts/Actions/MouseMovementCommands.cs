using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementCommands : ICommand
{
    private Rigidbody m_rigidbody;
    private Camera m_camera;

    private GameObject m_destination;

    public MouseMovementCommands(GameObject obj)
    {
        Obj = obj;
        m_rigidbody = Obj.GetComponent<Rigidbody>();
        m_camera = Obj.transform.GetChild(0).GetComponent<Camera>();

        //Create the destination game object
        m_destination = new GameObject("Target Destination");
    }

    public void Execute()
    {
        RaycastHit hit;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //Checks if the hit object should be traversable
            if (hit.transform.tag == "TraversableGround")
            {
                //Moves the destination game object to the provided location
                m_destination.transform.position = hit.point;

                //Applies velocity to the character in the direction of the destination
                m_rigidbody.velocity = GetDirectionToTarget(m_destination);
            }
        }
    }

    private Vector3 GetDirectionToTarget(GameObject target)
    {
        Vector3 direction = target.transform.position - Obj.transform.position;

        return direction;
    }

    public GameObject Obj { get; set; }
}