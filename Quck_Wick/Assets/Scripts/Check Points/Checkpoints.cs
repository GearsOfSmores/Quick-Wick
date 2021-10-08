using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private GameMaster gm;

    
    void Start()
    {

        // Setting the game master veriable to save the checkpoints
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Looks for the player when triggered saves the position of the last checkpoint the player touched
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
        }
    }
}
