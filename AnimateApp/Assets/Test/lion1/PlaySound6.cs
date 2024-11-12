using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound6 : MonoBehaviour
{
    public AudioSource audioSource;

  

    // ฟังก์ชั่นนี้จะถูกเรียกเมื่อ Animation Event เกิดขึ้น
    public void PlaySoundEffect6()
    {
        audioSource.Play();
    }
      public void StopSoundEffect6()
    {
        audioSource.Stop();
    }
}
