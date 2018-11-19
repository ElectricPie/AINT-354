using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTestButton : MonoBehaviour, IInteractable {

    public void Interact()
    {
        Debug.Log("Interacting with box");
    }
}
