using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float delayTime;
    public float blinkTime;

    private float timer;

    private bool delayDone = false;
    public bool destroyOnDone = false;

	// Use this for initialization
	void Start ()
    {
        timer = delayTime;
        delayDone = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!delayDone)
            {
                delayDone = true;
                timer += blinkTime;
            }
            else
            {
                if (destroyOnDone)
                {
                    Destroy(gameObject);
                }
                return;
            }
        }

        if (delayDone && spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.clear, Mathf.Sin((blinkTime - timer) / blinkTime * Mathf.PI));
        }

	}
}
