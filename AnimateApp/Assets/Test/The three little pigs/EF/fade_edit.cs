using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade_edit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool _fadeIn = false;
    private bool _fadeOut = false;

    public float timeToFade = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeIn)
        {
            if (spriteRenderer.color.a < 1)
            {
                Color color = spriteRenderer.color;
                color.a += timeToFade * Time.deltaTime;
                spriteRenderer.color = color;

                if (spriteRenderer.color.a >= 1)
                {
                    _fadeIn = false;
                }
            }
        }
        if (_fadeOut)
        {
            if (spriteRenderer.color.a > 0)
            {
                Color color = spriteRenderer.color;
                color.a -= timeToFade * Time.deltaTime;
                spriteRenderer.color = color;

                if (spriteRenderer.color.a <= 0)
                {
                    _fadeOut = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        _fadeIn = true;
    }

    public void FadeOut()
    {
        _fadeOut = true;
    }
}
