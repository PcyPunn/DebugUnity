using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound5 : MonoBehaviour
{
    public AudioSource audioSource;

  

    // ฟังก์ชั่นนี้จะถูกเรียกเมื่อ Animation Event เกิดขึ้น
    public void PlaySoundEffect5()
    {
        audioSource.Play();
    }
      public void StopSoundEffect5()
    {
        audioSource.Stop();
    }
}
