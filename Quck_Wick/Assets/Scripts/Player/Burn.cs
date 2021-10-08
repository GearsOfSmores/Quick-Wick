using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Burnable")
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "Torch")
        {
            other.GetComponent<Torch>().Light();
        }
    }
}
