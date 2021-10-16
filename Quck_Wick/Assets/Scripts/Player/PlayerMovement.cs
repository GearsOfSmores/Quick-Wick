using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    public Vector2 direction;
    public bool facingRight = true;
    public GameObject player;
    public GameObject[] crates;


    [Header("Jump")]
    [SerializeField] float jumpSpeed = 12f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    public float airRightSpeed = 5f;
    public float airLeftSpeed = 10f;
    public bool inAir = false;


    [Header("Enemy")]
    public float enemyDamage;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;
    public Vector3 leftOffset;
    public Vector3 rightOffset;
    public bool isPulling = false;
    public bool isMovingObject = false;

    [Header("Components")]
    private SpriteRenderer sr;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    public Animator playerAnimator;
    Collider2D myCollider2D;
    Rigidbody2D myRigidBody;
    public SpriteRenderer bodySr;
    public GameObject gameManager;
    public float candleCounter;
    public GameObject _dustParticlePrefab;


    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public float glideGravity = .1f;


    private bool lastDir;
    
    private GameObject box;
    private float xPos;
    bool isDead = false;
    bool hasResponded = false;
    bool respond = false;


    [Header("Moveable Objects")]
    public float distance = 1f;
    public LayerMask boxMask;
    public float objectheight = 5f;
    public bool holdingObject = false;



    [Header("Collison Force")]
    public float knockback = 10f;
    public float knockbackLength;
    public float knockbackAir = 5f;
    public float knockbackCount = 0f;


    [Header("Burn Components")]
    public GameObject burnArea;
    public GameObject flame;
    public bool burning = false;


    [Header("Iframes")]
    [SerializeField] private float IframesDuration;
    [SerializeField] private int numberOfFlashes;


    [Header("Glide")]
    public bool gliding;
    public bool canGlide = false;

    private bool hasStartedPushing = false;
    private bool knockbackSFXPlayed = false;

    [SerializeField] Vector2 deathKick = new Vector2(.5f, .1f);

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    private void Start()
    {
        //isDead = false;
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
        crates = GameObject.FindGameObjectsWithTag("Grabbable");
        
    }

    void Update()
    {

        // So you cant keep moving the player after dying
        if (isDead) { return; }

        //FlipSprite(respond);
        // Adding a small delay to the jump, that enhances the feel of the jump when in game
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
         if (canGlide == true)
            {
                Glide();
            }
        
        Burn();

        // Added for if the player touches an enemy he will respond to the last checkpoint touched
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            knockbackCount = .2f;
            gameManager.GetComponent<MeltingTimer>().candleCounter -= enemyDamage;
            StartCoroutine(Invulnerability());

        }

        
        if (Input.GetKeyDown(KeyCode.X))
        {
            //StopPulling();
            respond = Respond();
            
        }
        if(transform.position.y < -20)
        {
            //StopPulling();
            respond = Respond();
        }

        FlipSprite(respond);

        // After the player responds set respond back to false so he doesn't keep jumping back to checkpoint
        if (respond)
        {
            respond = false;
        }
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

    // Respond has a small player death effect just so I knew what was happening when he died 
    public bool Respond()
    {
        // Calls the player position script to reset the player to the last checkpoint
        FindObjectOfType<PlayerPosition>().Die();
        hasResponded = true;
        return hasResponded;
    }


    private void Jump()
    {
        //To stop character from jumping in air
        // Made change to if he is pulling something then he cant jump - Shane
        if (onGround)
        {
            SoundManagerScript.PlaySound("jump");

            //Create a new Vector and addForce vertically to the character
             rb.velocity = new Vector2(rb.velocity.x , 0);

             if(Mathf.Abs(direction.x) < 0.4f)
            {
                rb.AddForce(Vector2.up * jumpSpeed *1.3f, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
             

            
            // resetting the jumpTimer to prevent multiple jumps
            jumpTimer = 0;

           // StartCoroutine(JumpSqueeze(/*0.5f*/transform.localScale.x - .5f, 1.2f, 0.1f));
        }
        
        
    }

    // Code for moving the character left and right
    private void Move(float horizontal)
    {
        if (onGround)
        {
            inAir = false;
        }
        else inAir = true;

       // if(inAir == true)
       // {
           // rb.velocity = new Vector2(rb.velocity.x * 2 * Time.deltaTime, rb.velocity.y);
       // }


        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Chekcing onGround with boolean and using raycast line to know when character is on ground
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer);

        // Multiplying the horizontal input by a variable we can change in the inspector
        if (knockbackCount <= 0 && inAir == false)
        {
            rb.AddForce(Vector2.right * horizontal * moveSpeed );
        }
        else if (knockbackCount <= 0 && inAir == true && facingRight)
        {
            rb.AddForce(Vector2.right * horizontal * moveSpeed *airRightSpeed);
        }
        else if(knockbackCount <= 0 && inAir == true && !facingRight)
        {
            rb.AddForce(Vector2.right * horizontal * moveSpeed * airLeftSpeed);
        }
         else if (knockbackCount > 0 && facingRight == true)
        {
            rb.velocity = new Vector2(-knockback, knockback);
            knockbackCount -= Time.deltaTime;
            StartCoroutine("KnockbackSFX");
        }
        else if (knockbackCount > 0 && facingRight == false)
        {
            rb.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
            StartCoroutine("KnockbackSFX");
        }


        // This result is a feeling of the character gaining momentum overtime
        // If current velocity is greater than our maxSpeed

        if (Mathf.Abs(rb.velocity.x) > maxSpeed && inAir == false)
        {
            // return the current velocity as 1 and multiply it by maxSpeed, clamping the speed.
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed , rb.velocity.y);
        }


        if (!wasOnGround && onGround)
        {
            //Landing code for spawning landing dust
            Vector3 offset = new Vector3(0f, 1.5f, 0f);
            GameObject dustClone = Instantiate(_dustParticlePrefab, this.transform.position - offset, Quaternion.identity);
            Destroy(dustClone, 1f);
            //Changed the x position to take the current x position and add to it - Shane
            //StartCoroutine(JumpSqueeze(transform.localScale.x + .25f, 0.8f, 0.05f));
        }
       
        

        if(!wasOnGround && onGround && Mathf.Abs(direction.x) < 0.4f)
        {
            rb.velocity = new Vector2(rb.velocity.x / 5, 0); //Added this code to make the landing less slippery.
        }

        playerAnimator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        playerAnimator.SetFloat("vertical", rb.velocity.y);

        if (isMovingObject && !hasStartedPushing && Input.GetAxis("Horizontal") > 0 && onGround || isMovingObject && !hasStartedPushing && Input.GetAxis("Horizontal") < 0 && onGround)
        {
            hasStartedPushing = true;
            StartCoroutine("PushingSFX");
        }
    }

    // To flip the character when they are going right or left
    // Changed it so that the player is being flipped by the scale instead of the sprite renderer
    // Added a bool veriable to tell if the player has responded
    private void FlipSprite(bool hasResponded)
    {
        //Debug.Log("has responded: " + hasResponded);
        // Had to check for if the key was being pressed down twice in the code for there to be no errors when 
        // grabbing an item. Once in here and again in the pull method
        if (Input.GetKeyDown(KeyCode.W) && !isMovingObject)
        { 
            isPulling = Pull(hasResponded);
            if (hasResponded)
            {
                // Had to add this to force the pulling to stop for him to stop if he responded
                isPulling = false;
            }
        }

        // Added this for if the player hits r they will release the object
        else if (Input.GetKeyDown(KeyCode.W) && isMovingObject || hasResponded)
        {
            isPulling = StopPulling(hasResponded);
        }

        //Debug.Log("has pulled: " + isPulling);
        // If the character is not pulling or pushing then he will keep turning left and right like normal
        if (!isPulling)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                facingRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
            }
        }

        // If he is holding something he will not turn left or right
        else if(isPulling)
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        }

    }


    // This method will allow the player to push and pull and object box mask in the inspector will need to have
    // everything selected except for player

    // Added a bool variable to pass through the function to tell if the player responded
    private bool Pull(bool responded)
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);

        // If there is no object if front of you it wont try and grab
        if (hit.collider != null)
        {
            // If the object is tagged Grabbable in the inspector and not null and pressing e then grab the item
            // Objects that need to be grabbed will have to have component Fixed Joint 2D and it will have to be
            // inactive before starting. If active player will be stuck to the object

            // Made a small change here to make sure the player didn't respond
            if (hit.collider != null 
                && hit.collider.gameObject.tag == "Grabbable" 
                && Input.GetKeyDown(KeyCode.W) && !isMovingObject
                && !responded)
            {
                
                box = hit.collider.gameObject;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<PullingBox>().pushing = true;
                isMovingObject = true;
              
                return true;
                
            }


        }

        // Removed the get key up code so the player will continue to hold the object until r is pressed

        return false;

    }
    // The new function for when the player hits r then he should release the object
    private bool StopPulling(bool hasResponded)
    {
        // Updated so it checks for if the player has responded if he has then release the object
        if (Input.GetKeyDown(KeyCode.W) && isMovingObject && !hasResponded || hasResponded && isMovingObject)
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<PullingBox>().pushing = false;
            isMovingObject = false;
            return false;
        }

        // Else statement returning false this portion for some reason if I take it out the player wont release
        // the crate if you respond without a crate
        else
        {
            return false;
        }
    }

    private void ThrowObject()
    {
   
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<PullingBox>().pushing = false;
            box.GetComponent<Rigidbody2D>().AddForce(Vector2.up * objectheight *Time.deltaTime, ForceMode2D.Impulse);


    }


    //This method will create a 'better feeling' jump that 
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);


        if (onGround)
        {
            inAir = false;
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


    private void Glide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            gliding = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !onGround && gliding)
        {
            gravity = glideGravity;
            playerAnimator.SetBool("gliding", true);
        }
        else if (onGround)
        {
            gliding = false;
            gravity = 1f;
            playerAnimator.SetBool("gliding", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !onGround)
        {
            gravity = 1f;
            playerAnimator.SetBool("gliding", false);
            gliding = false;
        }
    }

    private void Burn()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            burning = true;
            burnArea.SetActive(true);
            SoundManagerScript.PlaySound("burn");
            flame.transform.localScale = flame.transform.localScale * 1.5f;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            burning = false;
            burnArea.SetActive(false);
            flame.transform.localScale = flame.transform.localScale / 1.5f;
        }
    }


    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 12, true);
        //invulnerability duration

        for (int i = 0; i < numberOfFlashes; i++)
        {
            bodySr.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes * 2));
            bodySr.color = Color.white;
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 12, false);
    }

    private IEnumerator PushingSFX()
    {
        while(hasStartedPushing && Input.GetAxis("Horizontal") > 0 && onGround || hasStartedPushing && Input.GetAxis("Horizontal") < 0 && onGround)
        {
            SoundManagerScript.PlaySound("push");
            yield return new WaitForSeconds(.7f);
        }
        hasStartedPushing = false;
    }

    private IEnumerator KnockbackSFX()
    {
        if(!knockbackSFXPlayed)
        {
            SoundManagerScript.PlaySound("ouch");
            knockbackSFXPlayed = true;
        }
        yield return new WaitForSeconds(.1f);
        knockbackSFXPlayed = false;
        StopCoroutine("KnockbackSFX");
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
}