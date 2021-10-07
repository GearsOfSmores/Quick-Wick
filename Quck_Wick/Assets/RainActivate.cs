using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainActivate : MonoBehaviour
{
    public GameObject rain2;
    public bool rainStayActive;
    private void Awake()
    {
        rain2 = GameObject.Find("Rain2");
        rain2.SetActive(false);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rain2.SetActive(true);
            
        }
    }
}
