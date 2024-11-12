using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI; // สำหรับควบคุม UI
using System.IO;

public class AudioRecordRnT1st1 : MonoBehaviour
{
    private List<Animator> animators = new List<Animator>();

    public float stopTime = 10f; // เวลาในวินาทีที่ต้องการหยุด  
    public Button recordButton; // ปุ่ม Pause/Resume
    public GameObject timeRecord;
    public GameObject lineRecord;
    public GameObject bgRecord;
    public AudioSource audioSource; // AudioSource สำหรับเล่นเสียงที่บันทึก
    private AudioClip recordedClip; // เก็บเสียงที่บันทึก
    private string filePath; // ที่เก็บไฟล์เสียง

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "rabbit1st1.wav");

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        animators.AddRange(FindObjectsOfType<Animator>());

        foreach (Animator animator in animators)
        {
            animator.speed = 1;
        }

        recordButton.gameObject.SetActive(false);
        lineRecord.gameObject.SetActive(false);
        bgRecord.gameObject.SetActive(false);

        recordButton.onClick.AddListener(StartRecording);

        StartCoroutine(StopAllAfterTime(stopTime));
    }

    IEnumerator StopAllAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (Animator animator in animators)
        {
            animator.speed = 0;
        }

        recordButton.gameObject.SetActive(true);
        lineRecord.gameObject.SetActive(true);
        bgRecord.gameObject.SetActive(true);
    }

    void PlayAnimation()
    {
        Debug.Log("Animation is Playing.");
        Microphone.End(null);
        recordButton.gameObject.SetActive(false);
        timeRecord.gameObject.SetActive(false);
        lineRecord.gameObject.SetActive(false);
        bgRecord.gameObject.SetActive(false);

        foreach (Animator animator in animators)
        {
            animator.speed = 1;
        }

        SaveRecordedAudio(); // บันทึกเสียงลงไฟล์
        PlayRecordedAudio();
    }

    public void StartRecording()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone devices found.");
            return;
        }

        Debug.Log("Audio is Recording.");
        recordedClip = Microphone.Start(null, false, 8, 44100);
        timeRecord.gameObject.SetActive(true);

        if (recordedClip == null)
        {
            Debug.LogError("Failed to start recording.");
            return;
        }

        Invoke("PlayAnimation", 8);
    }

    void SaveRecordedAudio()
    {
        var audioData = WavUtility.FromAudioClip(recordedClip);
        File.WriteAllBytes(filePath, audioData);
        Debug.Log("Audio saved to: " + filePath);       
    }

    void PlayRecordedAudio()
    {
        Debug.Log("Audio is Playing.");
        audioSource.clip = recordedClip;
        audioSource.Play();
    }
}