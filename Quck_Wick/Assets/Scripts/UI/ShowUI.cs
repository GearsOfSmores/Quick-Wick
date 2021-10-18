using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public GameObject text;
    public GameObject textBackground;
    //public GameObject moveText;
   
 
   // public bool destroyMove;
   // public bool jumpTextActive = false;

    //public Collider2D jumpCollider;
    //public GameObject UIArt;
    //public GameObject UIArtTwo;
    //public bool moveTextActive = true;
    //public ShowUI uiShow;
    private void Start()
    {
        text.SetActive(false);
        textBackground.SetActive(false);
        //jumpText.SetActive(false);
       // UIArt.SetActive(false);
        //destroyMove = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            text.SetActive(true);
            textBackground.SetActive(true);
        }
    }

    /*

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player" && destroyMove == false)
        {
            UIArt.SetActive(true);
            jumpText.SetActive(true);
            StartCoroutine("JumpWait");
              
        }
        else if (collision.gameObject.tag == "Player" && destroyMove == true)
            {
          
           
            moveText.SetActive(false);
                jumpText.SetActive(true);
                UIArt.SetActive(true);
                StartCoroutine("JumpWait");
            }

    }

    IEnumerator JumpWait()
    {
        yield return new WaitForSeconds(20);
        
        jumpText.SetActive(false);
        UIArt.SetActive(false);
        
    }

    IEnumerator DestroyMove()
    {
        if (uiShow.jumpTextActive == false)
            yield return new WaitForSeconds(20);
        //Destroy(moveText);
        
        moveTextActive = false;
    }
    */
}
