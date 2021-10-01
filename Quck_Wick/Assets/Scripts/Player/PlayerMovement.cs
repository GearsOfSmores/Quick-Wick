﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    public Vector2 direction;


    [Header("Jump")]
    [SerializeField] float jumpSpeed = 12f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;


    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;
    public Vector3 leftOffset;
    public Vector3 rightOffset;

    [Header("Components")]
    private SpriteRenderer sr;
    public LayerMask groundLayer;
    public Rigidbody2D rb;


    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public float glideGravity = .1f;


    private bool lastDir;
    private bool inAir = false;


    Rigidbody2D myRigidBody;
    Collider2D myCollider2D;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
    }

     void Update()
    {
        
        // Adding a small delay to the jump, that enhances the feel of the jump when in game
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        
        FlipSprite();
        Glide();
    }

     void FixedUpdate()
    {
        if (jumpTimer > Time.time)
        {
            Jump();
        }
        modifyPhysics();
        Move(direction.x);
    }

  
    private void Jump()
    {
        //To stop character from jumping in air
        if (onGround)
        {
            //Create a new Vector and addForce vertically to the character
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            
            // resetting the jumpTimer to prevent multiple jumps
            jumpTimer = 0;

            StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
        }
    }

    // Code for moving the character left and right
    private void Move(float horizontal)
    {
        
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Chekcing onGround with boolean and using raycast line to know when character is on ground
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer);

        // Multiplying the horizontal input by a variable we can change in the inspector
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        // This result is a feeling of the character gaining momentum overtime
        // If current velocity is greater than our maxSpeed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            // return the current velocity as 1 and multiply it by maxSpeed, clamping the speed.
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

       
        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

    }

    // To flip the character when they are going right or left
    private void FlipSprite()
    {
        // Uses the sprite renderer to check if the player is going right or left
        // check the flip check in the sprite renderer
        if (Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
            lastDir = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
            lastDir = false;
        }
        else
        {
            sr.flipX = lastDir;
        }
    }


     //This method will create a 'better feeling' jump that 
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);


        if (onGround)
        {
            // Applying linear drag to the character after. 
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            // When moving onGround, gravity is set to zero.
            rb.gravityScale = 0;
        }
        else
        {
            // when Jumping, gravity is changed back to 1
            rb.gravityScale = gravity;

            // Linardrag is reduced in the air
            rb.drag = linearDrag * 0.15f;

            // Once the character reaches the peak of the jump, we add a fallMulitper to bring the character back down faster
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }

            // If holding down the jump button, before reaching the peak of the jump, gravity is divided by 2.
            // Resulting in a higher jump
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    // Squeezes the spriterender when both jumping and landing 
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        // tracks current localscale of sr size
        Vector3 originalSize = Vector3.one;
        // create and define new size
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            sr.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            sr.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }

    private void Glide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            gravity = glideGravity;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            gravity = 1f;
        }
    }
    private void OnDrawGizmos()
    {
        // Drawing a red line from center origin of GameObject to visually represent the RayCast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
    }
}