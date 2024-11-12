using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI; // สำหรับควบคุม UI

public class AudioRecordController : MonoBehaviour
{
    // เก็บ Animator และ AudioSource ใน List
    private List<Animator> animators = new List<Animator>();

    public float stopTime = 10f; // เวลาในวินาทีที่ต้องการหยุด  
    public Button recordButton; // ปุ่ม Pause/Resume
    public GameObject timeRecord;
    public GameObject lineRecord;
    public GameObject bgRecord;
    public AudioSource audioSource;       // AudioSource สำหรับเล่นเสียงที่บันทึก
    private AudioClip recordedClip;       // เก็บเสียงที่บันทึก


    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        animators.AddRange(FindObjectsOfType<Animator>());

        foreach (Animator animator in animators)
        {
            animator.speed = 1;
        }

        // ซ่อนปุ่ม Pause ตอนเริ่มต้น
        recordButton.gameObject.SetActive(false);
        lineRecord.gameObject.SetActive(false);
        bgRecord.gameObject.SetActive(false);

        // เชื่อมปุ่มกับฟังก์ชัน
        recordButton.onClick.AddListener(StartRecording);

        // เริ่มนับเวลา
        StartCoroutine(StopAllAfterTime(stopTime));
    }

    IEnumerator StopAllAfterTime(float time)
    {
        // รอเป็นเวลา 'time' วินาที
        yield return new WaitForSeconds(time);

        // หยุดการทำงานของ Animator และ AudioSource ทั้งหมด
        foreach (Animator animator in animators)
        {
            animator.speed = 0;
        }

        // แสดงปุ่ม Pause/Resume
        recordButton.gameObject.SetActive(true);
        lineRecord.gameObject.SetActive(true);
        bgRecord.gameObject.SetActive(true);
    }

    // ฟังก์ชันเปิด/ปิด Animator และ AudioSource เมื่อกดปุ่ม
    void PlayAnimation()
    {
        Debug.Log("Animation is Playing.");
        Microphone.End(null);  // หยุดบันทึก
        recordButton.gameObject.SetActive(false);  // ซ่อนปุ่มบันทึก
        timeRecord.gameObject.SetActive(false);
        lineRecord.gameObject.SetActive(false);
        bgRecord.gameObject.SetActive(false);

        foreach (Animator animator in animators)
        {
            animator.speed = 1;
        }

        PlayRecordedAudio();

    }

    public void StartRecording()
    {
        if (Microphone.devices.Length == 0)  // ตรวจสอบว่ามีไมโครโฟนในอุปกรณ์หรือไม่
        {
            Debug.LogError("No microphone devices found.");
            return;
        }

        Debug.Log("Audio is Recording.");
        // เริ่มบันทึกเสียง (สูงสุด 8 วินาที ที่ความถี่ 44100 Hz)
        recordedClip = Microphone.Start(null, false, 8, 44100);
        timeRecord.gameObject.SetActive(true);

        if (recordedClip == null)  // ตรวจสอบว่าการบันทึกสำเร็จหรือไม่
        {
            Debug.LogError("Failed to start recording.");
            return;
        }

        Invoke("PlayAnimation", 8);  // หยุดบันทึกอัตโนมัติหลัง 8 วินาที
    }

    void PlayRecordedAudio()
    {
        Debug.Log("Audio is Playing.");
        audioSource.clip = recordedClip;  // ใส่เสียงที่บันทึกใน Aud ioSource
        audioSource.Play();  // เล่นเสียง
    }
}