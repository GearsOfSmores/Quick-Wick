using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public RopeDestroy RD;

    private void Update()
    {
        if(RD.destroyRope == true)
        {
            Destroy(gameObject);
        }
    }
}
