using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        
    }

    // �ѧ���蹹��ж١���¡����� Animation Event �Դ���
    public void PlaySoundEffect()
    {
        audioSource.Play();
    }
}