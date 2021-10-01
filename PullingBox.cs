using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingBox : MonoBehaviour
{
    // Variables to set the mass if we want bigger objects it will move slower
    public float baseMass;
    public float moveableMass;
    public bool pushing;
    float xPos;

    public Vector3 lastPos;

    public int mode;
    public int collid;

    
    void Start()
    {
        xPos = transform.position.x;
        lastPos = transform.position;
    }

    
    void FixedUpdate()
    {

        // Checking to see if the object is able to be moved
        if (mode == 0)
        {
            if (pushing == false)
            {
                transform.position = new Vector3(xPos, transform.position.y);
            }
            else
            {
                xPos = transform.position.x;
            }
        }

        else if (mode == 1)
        {
            if (pushing == false)
            {
                GetComponent<Rigidbody2D>().mass = moveableMass;

            }
            else
            {
                GetComponent<Rigidbody2D>().mass = baseMass;
                GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
