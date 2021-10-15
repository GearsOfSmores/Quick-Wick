using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateReset : MonoBehaviour
{
    public Vector2[] cratePos;
    public GameObject crate;

    // To check what Items are in the crate array
    public GameObject[] crates;
    int crateArrayLength;
    
    void Start()
    {
        int count = 0;

        // This creates an array of all items tagged grabbable
        crates = GameObject.FindGameObjectsWithTag("Grabbable");
        crateArrayLength = crates.Length;

        // This creates an array with the size of the previous array for crate position
        cratePos = new Vector2[crateArrayLength];

        // For each statement to get the position of each item tagged grabbable from the transform
        foreach (GameObject pos in crates)
        {
            cratePos[count] = pos.transform.position;
            count++;
        }
    }

    
    void Update()
    {

    }

    // Function for if called will reset the position of all the crates in the scene
    public void crateReset()
    {
        for(int i = 0; i < crates.Length; i++)
        {
            crates[i].transform.position = cratePos[i];
        }
    }
}
