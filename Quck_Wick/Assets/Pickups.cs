using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private bool canGlide;
    SpriteRenderer sr;
    private void Start()
    {
       
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player")
        {
            playerMovement.GetComponent<PlayerMovement>().canGlide = true;

            gameObject.SetActive(false);
        }



    }
}
