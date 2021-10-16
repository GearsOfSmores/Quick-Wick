using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    public bool OnPlatform = false;

    [SerializeField] private BoxCollider2D playerCollider;
     

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine("DisableCollision");
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay"))
        {
            OnPlatform = true;
            currentOneWayPlatform = collision.gameObject;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay"))
        {
            currentOneWayPlatform = null;
            OnPlatform = true;
        }
    }

    private IEnumerator DisableCollision()
    {
        EdgeCollider2D platformCollider = currentOneWayPlatform.GetComponent<EdgeCollider2D>();

        platformCollider.enabled = false;
        yield return new WaitForSeconds(.5f);
        platformCollider.enabled = true;

    }
}
