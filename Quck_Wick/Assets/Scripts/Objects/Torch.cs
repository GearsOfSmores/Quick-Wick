using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject flame;
    public GameObject light;
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
        light.SetActive(true);
        flame.SetActive(true);
        if (glideArea != null)
        {
            glideArea.SetActive(true);
        }
    }
}
