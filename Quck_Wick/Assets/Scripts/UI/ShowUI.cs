using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public GameObject jumpText;
    public GameObject moveText;
   
 
    public bool destroyMove;
    public bool jumpTextActive = false;

    public Collider2D jumpCollider;

    public bool moveTextActive = true;
    public ShowUI uiShow;
    private void Start()
    {
      
        jumpText.SetActive(false);

        destroyMove = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player" && destroyMove == false)
        {
            
            jumpText.SetActive(true);
            StartCoroutine("JumpWait");
              
        }
        else if (collision.gameObject.tag == "Player" && destroyMove == true)
            {

                moveText.SetActive(false);
                jumpText.SetActive(true);
                StartCoroutine("JumpWait");
            }

    }
    IEnumerator JumpWait()
    {
        yield return new WaitForSeconds(5);
        Destroy(jumpText);
    }

    IEnumerator DestroyMove()
    {
        if (uiShow.jumpTextActive == false)
            yield return new WaitForSeconds(3);
        Destroy(moveText);
        moveTextActive = false;


    }

}
