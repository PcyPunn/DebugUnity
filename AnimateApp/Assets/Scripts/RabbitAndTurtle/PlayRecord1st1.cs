using System.IO;
using UnityEngine;

public class PlayRecord1st1 : MonoBehaviour
{
    public AudioSource audioSource;
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "rabbit1st1.wav");

        // ตรวจสอบว่าไฟล์เสียงมีอยู่หรือไม่
        if (File.Exists(filePath))
        {
            LoadAndPlayAudio();
        }
        else
        {
            Debug.LogWarning("No recorded audio file found.");
        }
    }

    void LoadAndPlayAudio()
    {
        var audioData = File.ReadAllBytes(filePath);
        var loadedClip = WavUtility.ToAudioClip(audioData);
        audioSource.clip = loadedClip;
        audioSource.Play();
    }
}
