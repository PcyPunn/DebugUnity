using UnityEngine;
using System.Collections;

public class Play5Sound : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public AudioSource audioSource5;
    public float delayTime1 = 1.0f; // เวลาที่จะดีเลย์เสียงแรก
    public float delayTime2 = 2.5f; // เวลาที่จะดีเลย์เสียงที่สอง\
    public float delayTime3 = 2.5f;
    public float delayTime4 = 2.5f;
    public float delayTime5 = 2.5f;

    void Start()
    {
        StartCoroutine(PlayAudioWithDelay(audioSource1, delayTime1));
        StartCoroutine(PlayAudioWithDelay(audioSource2, delayTime2));
        StartCoroutine(PlayAudioWithDelay(audioSource3, delayTime3));
        StartCoroutine(PlayAudioWithDelay(audioSource4, delayTime4));
        StartCoroutine(PlayAudioWithDelay(audioSource5, delayTime5));
    }

    IEnumerator PlayAudioWithDelay(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
