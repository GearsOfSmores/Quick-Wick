using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeltingTimer : MonoBehaviour
{
    public float candleCounter;

    public float initialCounter;

    public bool byBonfire;

    public GameObject player;

    public TMP_Text timer;

    public ParticleSystem fireParticle;
    

    public GameObject wickFire;


    // Start is called before the first frame update
    void Start()
    {
        initialCounter = candleCounter;
    }

    // Update is called once per frame
    void Update()
    {
        //For future implementation for checkpoints being a chance for some breathing room.
        if(!byBonfire)
        {
            candleCounter -= Time.deltaTime;
            timer.text = Mathf.Round(candleCounter).ToString();
            var main = fireParticle.main;
            main.startSize = (.85f * candleCounter) / initialCounter;
            wickFire.transform.localScale = new Vector3((.85f * candleCounter) / initialCounter, (.85f * candleCounter) / initialCounter, (.85f * candleCounter) / initialCounter);
            //Has to be divided by the starting value of the candle counter.
            if (candleCounter <= 0)
            {
                player.gameObject.SetActive(false);
            }
        }
    }
}
