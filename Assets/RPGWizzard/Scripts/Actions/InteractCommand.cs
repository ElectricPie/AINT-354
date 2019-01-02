using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCommand : ICommand
{
    private Rigidbody m_rigidbody;
    private Camera m_camera;

    public InteractCommand(GameObject obj)
    {
        Obj = obj;
        m_camera = Obj.transform.GetChild(0).GetComponent<Camera>();
    }

    public void Execute()
    {
        RaycastHit hit;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //Check if the object has a interatable script
            if (hit.transform.GetComponent(typeof(IInteractable)))
            {
                //Calls the interact method
                hit.transform.GetComponent<IInteractable>().Interact();
            }
        }
    }

    public GameObject Obj { get; set; }
}
