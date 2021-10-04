using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    Rigidbody2D rb;
    public RopeDestroy RD;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.isKinematic = true;
    }

    private void Update()
    {
        if (RD.destroyRope == true)
        {
            rb.isKinematic = false;
        }
    }

}
