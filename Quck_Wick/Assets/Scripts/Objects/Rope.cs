using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public RopeDestroy RD;
    public GameObject BH;
    public PlayerMovement playerMovement;

    private void Start()
    {
        BH = GameObject.Find("RainTrigger/BottomHalf");
    }
    private void Update()
    {
        /*
        if(RD.destroyRope == true)
        {
            Destroy(gameObject);
            BH.SetActive(false);
        }*/

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && playerMovement.burning == true)
        {
           
            Destroy(gameObject);
            
        }

        

    }
}

