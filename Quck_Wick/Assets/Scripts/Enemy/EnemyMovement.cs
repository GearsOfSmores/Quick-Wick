using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private bool keepPatrolling;

    // To set the enemy move speed in the inspector
    public float moveSpeed;

    private bool turn;
    private bool wallTurn;

    public Vector3 colliderOffset;

    // To set the enemy rigid body in the inspector
    public Rigidbody2D rb;

    // The public variables to set the ground detection child and the wall detection child
    public Transform detectGoundPos;
    public Transform detectWallPos;

    // To set what the detections are looking for and the box collider
    // tried removing the collider but had issues with them going through walls when removed
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public float circleRadius;
    public Animator anim;

    void Start()
    {
        keepPatrolling = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If keep patrolling equals true then the enemy will keep going
        if (keepPatrolling)
        {
            Patrol();
        }
        Physics2D.IgnoreLayerCollision(12, 14);
        anim.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
    }

    void FixedUpdate()
    {
        if (keepPatrolling)
        {
            // If the enemy touches a wall or reaches the end of a platform then change there direction
            turn = !Physics2D.OverlapCircle(detectGoundPos.position, circleRadius, groundLayer);
            wallTurn = Physics2D.OverlapCircle(detectWallPos.position, circleRadius, groundLayer);
        }
    }

    private void Patrol()
    {
        // If the enemy hits anything or if they reach the end of the platform turn the enemy
        if (turn || bodyCollider.IsTouchingLayers(groundLayer) || wallTurn)
        {
            Flip();
        }
        rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        // To flip the enemy left and right using the local scale x
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed *= -1;
        keepPatrolling = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(transform.position +colliderOffset, circleRadius);
    }


}
