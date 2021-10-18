using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, burnSound, jumpSound, glideSound, pushSound;
    static AudioSource audioSrc;

    private void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("ouch");
        burnSound = Resources.Load<AudioClip>("burn");
        jumpSound = Resources.Load<AudioClip>("jump");
        glideSound = Resources.Load<AudioClip>("glidestart");
        pushSound = Resources.Load<AudioClip>("push");

        audioSrc = GetComponent<AudioSource>();

    }


    public static void PlaySound(string clip)
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
            case "glidestart":
                audioSrc.PlayOneShot(glideSound);
                break;
            case "push":
                audioSrc.PlayOneShot(pushSound);
                break;

        }
    }

}
