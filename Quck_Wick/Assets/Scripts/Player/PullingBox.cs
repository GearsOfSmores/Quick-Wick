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
    public bool onGround;
    public Vector3 colliderOffset;
    public float groundLength;

    public LayerMask groundLayer;
    public Vector3 lastPos;

    public int mode;
    public int collid;
    public GameObject bottomHalf;
     FixedJoint2D FJ;
    private PlayerMovement pm;
    public bool holdingObject;
   // public float isPulling = false;


    void Start()
    {
        xPos = transform.position.x;
        lastPos = transform.position;
        
        /* FJ = GetComponent<FixedJoint2D>();
        FJ.enabled = false;
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        pm.holdingObject = holdingObject;
        pm.*/
    }

    private void Update()
    {
        /*
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer);
        if (holdingObject == true)
        {
            
        }
        else FJ.enabled = false;*/
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rain")
        {
            bottomHalf.SetActive(false);
        }
    }
    private void OnDrawGizmos()
        {
            // Drawing a red line from center origin of GameObject to visually represent the RayCast
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);

           
        }
    
}