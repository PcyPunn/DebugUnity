using UnityEngine;

public class PlaySound2 : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {

    }

    // �ѧ���蹹��ж١���¡����� Animation Event �Դ���
    public void PlaySoundEffect2()
    {
        audioSource.Play();
    }
}