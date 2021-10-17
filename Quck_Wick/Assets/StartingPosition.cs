using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    
    public Vector2 startingPosition;
    public GameObject player;
        private void Awake()
    {
        player.transform.position = startingPosition;
    }
}
