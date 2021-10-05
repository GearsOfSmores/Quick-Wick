using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public bool rainHit;
    public GameManager GM;
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.tag == "Player")
        {
            var player = collison.GetComponent<PlayerMovement>();
            player.knockbackCount = player.knockbackLength;
            
        }
    }
}
