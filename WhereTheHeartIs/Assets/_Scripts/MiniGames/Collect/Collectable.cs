using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public bool goodObject;
    private bool playerGotMe = false;

    public const float TIMER_MAX = 0.7f;
    public float disappearTimer = TIMER_MAX;

    public bool PlayerGotMe()
    {
        return playerGotMe;
    }

    private void Start()
    {
        if (goodObject)
        {
            Sprite[] goodSprites = Resources.LoadAll<Sprite>("Art/Obsticals/Search Find Objects");
            spriteRenderer.sprite = goodSprites[(int)Random.Range(0, goodSprites.Length)];
        }
    }

    void Update ()
    {
        if (playerGotMe)
        {
            disappearTimer -= Time.deltaTime;

            if (goodObject)
            {
                spriteRenderer.color = new Color(1, 1, 1, disappearTimer / TIMER_MAX);
            }
            else
            {
                spriteRenderer.color = new Color(1, 0, 0, disappearTimer / TIMER_MAX);
            }

            if (disappearTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerGotMe = true;
        }
    }
}
