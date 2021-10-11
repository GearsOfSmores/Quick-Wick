using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject rope;
    public GameObject BH;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (rope == null)
        {
            BH.SetActive(false);
            rb.isKinematic = false;
        }
    }

}
