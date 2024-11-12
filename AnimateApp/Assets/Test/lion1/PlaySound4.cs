using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound4 : MonoBehaviour
{
    public AudioSource audioSource;

  

    // ฟังก์ชั่นนี้จะถูกเรียกเมื่อ Animation Event เกิดขึ้น
    public void PlaySoundEffect4()
    {
        audioSource.Play();
    }
      public void StopSoundEffect4()
    {
        audioSource.Stop();
    }
}
