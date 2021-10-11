using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{

    public PlayerMovement playerMovement;
    
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Burnable" && playerMovement.burning == true)
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "Torch")
        {


            other.GetComponent<Torch>().Light();
        }
     
    }
}
