using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MeltingTimer : MonoBehaviour
{
    public float candleCounter;

    public float initialCounter;

    public bool byBonfire;
    

    public GameObject player;
    public PlayerMovement playermovement;
    public TMP_Text timer;

    public ParticleSystem fireParticle;
    public ParticleSystem fireEmber;

    public ParticleSystem FireSmoke;

    public Light2D wickLight;
    public GameObject wickFire;

    public bool timerOn =false;


    // Start is called before the first frame update
    void Start()
    {
        initialCounter = candleCounter;
        byBonfire = false;
    

    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Temple" || scene.name == "TutorialBegin")
        {
            timerOn = false;
        }
        else
        {
            timerOn = true;
        }
        //For future implementation for checkpoints being a chance for some breathing room.
        if(byBonfire == false && timerOn == true )
        {
            candleCounter -= Time.deltaTime;
            timer.text = Mathf.Round(candleCounter).ToString();
            var main = fireParticle.main;
            var main2 = fireEmber.main;
            var main3 = FireSmoke.main;

            main.startSize = (.85f * candleCounter) / initialCounter;
            main2.startSize = (.85f * candleCounter) / initialCounter;
            main3.startSize = (.85f * candleCounter) / initialCounter;

            wickLight.pointLightOuterRadius = (8 * candleCounter) / initialCounter; //Change 8 to whatever the value of the outerradius is to start. 
            wickLight.pointLightInnerRadius = (3 * candleCounter) / initialCounter;
            if (!playermovement.burning)
            {
               wickFire.transform.localScale = new Vector3((.85f * candleCounter) / initialCounter, (.85f * candleCounter) / initialCounter, (.85f * candleCounter) / initialCounter);
            }

           
            //Has to be divided by the starting value of the candle counter.
            if (candleCounter <= 0)
            {
                player.gameObject.SetActive(false);
            }
        }
    }
}
