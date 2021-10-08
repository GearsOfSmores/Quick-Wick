using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideBoost : MonoBehaviour
{
    public PlayerMovement player;
    public Rigidbody2D rb;
    public float boostSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.gliding)
        {
            rb.AddForce(Vector2.up * boostSpeed);
        }
    }
}
