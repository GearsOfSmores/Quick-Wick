using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTutorial : MonoBehaviour
{
    public GameObject tutorial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Destroy(tutorial);
        }
    }
}
