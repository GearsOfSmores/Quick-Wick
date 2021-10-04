using System.Collections;
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
    public bool isPulling = false;

    [Header("Components")]
    private SpriteRenderer sr;
    public LayerMask groundLayer;
    public Rigidbody2D rb;


    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;


    private bool lastDir;
    private bool inAir = false;
    private GameObject box;
    private float xPos;


    [Header("Moveable Objects")]
    public float distance = 1f;
    public LayerMask boxMask;

    Rigidbody2D myRigidBody;
    Collider2D myCollider2D;

    [Header("Collison Force")]
    public float knockback = 10f;
    public float knockbackLength;
    public float knockbackAir = 5f;
    public float knockbackCount = 0f;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        FlipSprite();
        // Adding a small delay to the jump, that enhances the feel of the jump when in game
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        //Debug.Log("This is x: " + xPos);
        //FlipSprite();
    }

    void FixedUpdate()
    {
        if (jumpTimer > Time.time)
        {
            //Debug.Log("This is x: " + transform.localScale.x);
            Jump();
        }
        modifyPhysics();
        Move(direction.x);
    }


    private void Jump()
    {
        //To stop character from jumping in air
        // Made change to if he is pulling something then he cant jump - Shane
        if (onGround && !isPulling)
        {
            //Create a new Vector and addForce vertically to the character
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

            // resetting the jumpTimer to prevent multiple jumps
            jumpTimer = 0;

            StartCoroutine(JumpSqueeze(/*0.5f*/transform.localScale.x - .5f, 1.2f, 0.1f));
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
        if (knockbackCount <= 0)
        {
            rb.AddForce(Vector2.right * horizontal * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(-knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }
        

        // This result is a feeling of the character gaining momentum overtime
        // If current velocity is greater than our maxSpeed

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            // return the current velocity as 1 and multiply it by maxSpeed, clamping the speed.
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }


        if (!wasOnGround && onGround)
        {
            // Changed the x position to take the current x position and add to it - Shane
            StartCoroutine(JumpSqueeze(transform.localScale.x + .25f, 0.8f, 0.05f));
        }

    }

    // To flip the character when they are going right or left
    // Changed it so that the player is being flipped by the scale instead of the sprite renderer - Shane
    private void FlipSprite()
    {

        // Had to check for if the key was being pressed down twice in the code for there to be no errors when 
        // grabbing an item. Once in here and again in the pull method
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPulling = Pull();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            isPulling = Pull();
        }

        // If the character is not pulling or pushing then he will keep turning left and right like normal
        if (!isPulling)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        // If he is holding something he will not turn left or right
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        }

    }


    // This method will allow the player to push and pull and object box mask in the inspector will need to have
    // everything selected except for player
    private bool Pull()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);

        // If there is no object if front of you it wont try and grab
        if (hit.collider != null)
        {
            // If the object is tagged Grabbable in the inspector and not null and pressing e then grab the item
            // Objects that need to be grabbed will have to have component Fixed Joint 2D and it will have to be
            // inactive before starting. If active player will be stuck to the object
            if (hit.collider != null && hit.collider.gameObject.tag == "Grabbable" && Input.GetKeyDown(KeyCode.E))
            {
                box = hit.collider.gameObject;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<PullingBox>().pushing = true;
                return true;
            }

            // Once the e key is released let go of the object
            else if (Input.GetKeyUp(KeyCode.E))
            {
                box.GetComponent<FixedJoint2D>().enabled = false;
                box.GetComponent<PullingBox>().pushing = false;
                return false;

            }

        }

        return false;

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
            ///Resulting in a higher jump
           else if (rb.velocity.y > 0 && Input.GetButton("Jump") || !Input.GetButton("Jump"))
            {
               rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    // Squeezes the spriterender when both jumping and landing 
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        // tracks current localscale of sr size
        Vector3 originalSize = transform.localScale;
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


    private void OnDrawGizmos()
    {
        // Drawing a red line from center origin of GameObject to visually represent the RayCast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);

        // Drawing a green line to show where the player can grab distance can be changed in the inspector
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RainHit")
        {
            
           
        }
    }
}