using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typewriter : MonoBehaviour
{
    public GameObject txttt;
    TMP_Text txt;
    string story;
    // Start is called before the first frame update
    void Awake (){
        txt = txttt.GetComponent<TMP_Text>();
		story = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine ("PlayText");
    }

    IEnumerator PlayText()
	{
		foreach (char c in story) 
		{
			txt.text += c;
			yield return new WaitForSeconds (0.02f);
		}
	}
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
