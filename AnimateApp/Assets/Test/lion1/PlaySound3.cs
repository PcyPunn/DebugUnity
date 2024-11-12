using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound3 : MonoBehaviour
{
    public AudioSource audioSource;

  

    // ฟังก์ชั่นนี้จะถูกเรียกเมื่อ Animation Event เกิดขึ้น
    public void PlaySoundEffect3()
    {
        audioSource.Play();
    }
      public void StopSoundEffect3()
    {
        audioSource.Stop();
    }
}

