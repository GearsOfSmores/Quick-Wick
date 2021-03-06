using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{

    private GameObject[] objs;
    public bool caveMusic;
    
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Temple" || scene.name == "TutorialBegin")
        {
            caveMusic = true;
        }
        else
        {
            caveMusic = false;
        }
        objs = GameObject.FindGameObjectsWithTag("music");
        if (caveMusic == true)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
