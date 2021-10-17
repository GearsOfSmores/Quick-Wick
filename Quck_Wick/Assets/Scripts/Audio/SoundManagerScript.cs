using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, burnSound, jumpSound, glideSound;
    static AudioSource audioSrc;

    private void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("ouch");
        burnSound = Resources.Load<AudioClip>("burn");
        jumpSound = Resources.Load<AudioClip>("jump");
        glideSound = Resources.Load<AudioClip>("glidstart");

        audioSrc = GetComponent<AudioSource>();

    }
        

        public static void PlaySound (string clip)
        {
                switch (clip)
                {
                    case "ouch":
                        audioSrc.PlayOneShot(playerHitSound);
                        break;
                    case "burn":
                        audioSrc.PlayOneShot(burnSound);
                        break;
                    case "jump":
                        audioSrc.PlayOneShot(jumpSound);
                        break;
                    case "glidstart":
                        audioSrc.PlayOneShot(glideSound);
                        break;

        }
    }
    
}
