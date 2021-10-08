using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject flame;

    public GameObject glideArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Light()
    {
        flame.SetActive(true);
        if(glideArea != null)
        {
            glideArea.SetActive(true);
        }
    }
}
