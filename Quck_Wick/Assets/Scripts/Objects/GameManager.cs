using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Iframes")]
    [SerializeField] private float IframesDuration;
    [SerializeField] private int numberOfFlashes;
    public SpriteRenderer spriteRend;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }
    void Hit()
    {

    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        //invulnerability duration

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }


}
