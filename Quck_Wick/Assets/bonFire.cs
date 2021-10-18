using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonFire : MonoBehaviour
{
    public bool byBonFire = false;
    public MeltingTimer meltingTimer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            meltingTimer.byBonfire = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            meltingTimer.byBonfire = false;
        }
    }
}
