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
    public GameObject blackUIBox;
    public bool moveTextActive = true;
    public ShowUI uiShow;
    private void Start()
    {
      
        jumpText.SetActive(false);
        blackUIBox.SetActive(false);
        destroyMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player" && destroyMove == false)
        {
            blackUIBox.SetActive(true);
            jumpText.SetActive(true);
            StartCoroutine("JumpWait");
              
        }
        else if (collision.gameObject.tag == "Player" && destroyMove == true)
            {
            blackUIBox.SetActive(true);
            moveText.SetActive(false);
                jumpText.SetActive(true);
                StartCoroutine("JumpWait");
            }

    }
    IEnumerator JumpWait()
    {
        yield return new WaitForSeconds(12);
        blackUIBox.SetActive(false);
        jumpText.SetActive(false);
    }

    IEnumerator DestroyMove()
    {
        if (uiShow.jumpTextActive == false)
            yield return new WaitForSeconds(12);
        //Destroy(moveText);
        blackUIBox.SetActive(false);
        moveTextActive = false;
    }

}
