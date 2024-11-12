using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effctcontrol3 : MonoBehaviour
{
 [SerializeField] private ParticleSystem smokeParticleSystem;

    private bool _playeffect = false;
    private bool _stopeffect = false;

    public float smokeDuration = 3f;
    private float smokeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (smokeParticleSystem == null)
        {
            Debug.LogError("Smoke Particle System not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playeffect)
        {
            if (!smokeParticleSystem.isPlaying)
            {
                smokeParticleSystem.Play();
            }

            smokeTimer += Time.deltaTime;
            if (smokeTimer >= smokeDuration)
            {
                _playeffect = false;
                smokeTimer = 0f;
            }
        }

        if (_stopeffect)
        {
            if (smokeParticleSystem.isPlaying)
            {
                smokeParticleSystem.Stop();
            }

            _stopeffect = false; // Stop immediately, no need for a timer
        }
    }

    public void Playeffect3()
    {
        _playeffect = true;
    }

    public void Stopeffect3()
    {
        _stopeffect = true;
    }
}

