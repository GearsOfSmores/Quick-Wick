using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{

    private GameObject[] objs;
    private void Start()
    {


        objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)

            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);











    }
}
