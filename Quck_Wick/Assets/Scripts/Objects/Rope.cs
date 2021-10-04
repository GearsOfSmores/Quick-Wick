using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public RopeDestroy RD;
    public GameObject BH;

    private void Start()
    {
        BH = GameObject.Find("RainTrigger/BottomHalf");
    }
    private void Update()
    {
        if(RD.destroyRope == true)
        {
            Destroy(gameObject);
        }

    }
}
