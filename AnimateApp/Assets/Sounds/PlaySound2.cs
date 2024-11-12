using UnityEngine;

public class PlaySound2 : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {

    }

    // ฟังก์ชั่นนี้จะถูกเรียกเมื่อ Animation Event เกิดขึ้น
    public void PlaySoundEffect2()
    {
        audioSource.Play();
    }
}