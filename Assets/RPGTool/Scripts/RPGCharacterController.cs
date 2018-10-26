using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterController : MonoBehaviour {

    public float speed = 10;

    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        m_rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
	}
}
