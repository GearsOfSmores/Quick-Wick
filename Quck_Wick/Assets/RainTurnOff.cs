using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainTurnOff : MonoBehaviour
{

    public GameObject rainTrigger;
    public GameObject rain;
    public float RainOnTime = 4f;
    public float RainOffTime = 3f;
    private void Start()
    {
        StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOff()
    {
        while (true)
        {
            rainTrigger.SetActive(true);
            rain.SetActive(true);
            yield return new WaitForSeconds(RainOnTime);
            rainTrigger.SetActive(false);
            rain.SetActive(false);
            yield return new WaitForSeconds(RainOffTime);
        }
        
    }

}
