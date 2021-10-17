using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
   public string sceneToLoad;
        
    public Image blackImage;
   
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {

            StartCoroutine("FTB");
        }




    }
    public void Fade()
    {
        //This transitions from a black screen to a clear screen. Important to call after ToBlack, to avoid a permanently black screen.
        StartCoroutine("FadeFromBlack");
    }

    public void ToBlack()
    {
        //This can be called from a script whenever a new area or scene is loaded, or when we want a fade to black in game.
        StartCoroutine("FTB");
    }

    private IEnumerator FadeFromBlack()
    {
        while (blackImage.color.a > 0)
        {
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, blackImage.color.a - Time.deltaTime);
            yield return new WaitForSeconds(.01f);

        }


    }

    private IEnumerator FTB()
    {
        while (blackImage.color.a < 1)
        {
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, blackImage.color.a + Time.deltaTime);
            yield return new WaitForSeconds(.01f);

        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
