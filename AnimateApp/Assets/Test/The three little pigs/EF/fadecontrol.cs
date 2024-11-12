using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadecontrol : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasGroup;

    private bool _fadein = false;
    private bool _fadeout = false;

    public float TimeToFade = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_fadein)
        {
            if(CanvasGroup.alpha < 1)
            {
                CanvasGroup.alpha += TimeToFade*Time.deltaTime;
                if(CanvasGroup.alpha >= 1)
                {
                    _fadein =false;
                }
            }
        }
         if(_fadeout)
        {
            if(CanvasGroup.alpha >= 0)
            {
                CanvasGroup.alpha -= Time.deltaTime;
                if(CanvasGroup.alpha == 0)
                {
                    _fadeout =false;
                }
            }
        }
    }

    public void Fadein()
    {
        _fadein = true;
    }

    public void Fadeout() 
    {
        _fadeout = true;
    }   
}
