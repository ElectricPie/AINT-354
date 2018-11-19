using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileCharacter : Character
{
    [SerializeField]
    private GameObject m_playerCharacter;
    [SerializeField]
    private float m_aggroRange = 0;

    // Use this for initialization
    void Start()
    {
        //Find a player character
        m_playerCharacter = FindObjectOfType<PlayerCharacter>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerInRange();
    }

    private void CheckIfPlayerInRange()
    {
        //Prevents null refrences
        if (m_playerCharacter != null)
        {
            //Using sqrMagnitute as it is faster
            Vector3 offset = m_playerCharacter.transform.position - transform.position;
            float magnitudeDist = offset.sqrMagnitude;

            //Check if the player is within range
            if (magnitudeDist < m_aggroRange * m_aggroRange)
            {
                print("The other transform is close to me!");
                //Movement
                GetComponent<Rigidbody>().velocity = offset;
            }
        }
    }
}