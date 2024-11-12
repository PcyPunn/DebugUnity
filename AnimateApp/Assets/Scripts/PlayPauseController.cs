using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayPauseController : MonoBehaviour
{
    public Button playButton;
    public Button pauseButton;
    private List<Animator> animators = new List<Animator>();
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        animators.AddRange(FindObjectsOfType<Animator>());

        foreach (Animator animator in animators)
        {
            animator.speed = 1;
        }
        Time.timeScale = 1;

        // ค้นหา PlaySound scripts ทั้งหมดและเก็บ AudioSource ที่เกี่ยวข้อง
        PlaySound[] playSounds = FindObjectsOfType<PlaySound>();
        foreach (PlaySound playSound in playSounds)
        {
            if (playSound != null)
            {
                audioSources.Add(playSound.GetComponent<AudioSource>());
            }
        }

        // Debug เพื่อดูจำนวน AudioSource ที่ถูกเพิ่มเข้ามาในลิสต์
        Debug.Log("Total AudioSources: " + audioSources.Count);
        foreach (AudioSource audioSource in audioSources)
        {
            Debug.Log("AudioSource: " + audioSource.gameObject.name);
        }

        // ตรวจสอบว่าปุ่มถูกตั้งค่าใน Inspector
        if (playButton == null || pauseButton == null)
        {
            Debug.LogError("Buttons are not assigned in the Inspector.");
            return;
        }

        // เริ่มต้นด้วยการแสดงปุ่ม Play และซ่อนปุ่ม Pause
        ShowPauseButton();

        // เพิ่ม listener สำหรับปุ่ม Play และ Pause
        playButton.onClick.AddListener(OnPlayButtonClicked);
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    void OnPlayButtonClicked()
    {
        ShowPauseButton();

        foreach (Animator animator in animators)
        {
            animator.speed = 1;
        }

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.UnPause(); // ให้เสียงเล่นต่อจากจุดที่หยุด
        }

        // ตั้ง Time.timeScale เป็น 1 เพื่อให้ทุกสิ่งที่เกี่ยวข้องกับเวลาเริ่มทำงานอีกครั้ง
        Time.timeScale = 1;
    }

    void OnPauseButtonClicked()
    {
        ShowPlayButton();

        foreach (Animator animator in animators)
        {
            animator.speed = 0;
        }

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause(); // หยุดเสียงชั่วคราว
        }

        // ตั้ง Time.timeScale เป็น 0 เพื่อหยุดการทำงานของทุกสิ่งที่เกี่ยวข้องกับเวลา
        Time.timeScale = 0;
    }

    void ShowPlayButton()
    {
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    void ShowPauseButton()
    {
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
}