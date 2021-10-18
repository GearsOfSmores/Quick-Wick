using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    
    Collider2D box;
    
    public Vector3 SpawnLocation;
    public GameObject player;
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }
        else 
        {
            Destroy(gameObject);

        }
        
    }

    /*
     void Start()
    {
        player.transform.position = GameMaster.instance.SpawnLocation;
    }*/
}

