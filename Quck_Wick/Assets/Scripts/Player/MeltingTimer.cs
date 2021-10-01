using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeltingTimer : MonoBehaviour
{
    public float candleCounter;

    public bool byBonfire;

    public GameObject player;

    public TMP_Text timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //For future implementation for checkpoints being a chance for some breathing room.
        if(!byBonfire)
        {
            candleCounter -= Time.deltaTime;
            timer.text = Mathf.Round(candleCounter).ToString();
            if (candleCounter <= 0)
            {
                player.gameObject.SetActive(false);
            }
        }
    }
}
