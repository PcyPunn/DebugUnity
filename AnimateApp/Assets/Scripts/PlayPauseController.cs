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

        // ���� PlaySound scripts ����������� AudioSource �������Ǣ�ͧ
        PlaySound[] playSounds = FindObjectsOfType<PlaySound>();
        foreach (PlaySound playSound in playSounds)
        {
            if (playSound != null)
            {
                audioSources.Add(playSound.GetComponent<AudioSource>());
            }
        }

        // Debug ���ʹ٨ӹǹ AudioSource ���١������������ʵ�
        Debug.Log("Total AudioSources: " + audioSources.Count);
        foreach (AudioSource audioSource in audioSources)
        {
            Debug.Log("AudioSource: " + audioSource.gameObject.name);
        }

        // ��Ǩ�ͺ��һ����١��駤��� Inspector
        if (playButton == null || pauseButton == null)
        {
            Debug.LogError("Buttons are not assigned in the Inspector.");
            return;
        }

        // ������鹴��¡���ʴ����� Play ��Ы�͹���� Pause
        ShowPauseButton();

        // ���� listener ����Ѻ���� Play ��� Pause
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
            audioSource.UnPause(); // ������§��蹵�ͨҡ�ش�����ش
        }

        // ��� Time.timeScale �� 1 �������ء��觷������Ǣ�ͧ�Ѻ����������ӧҹ�ա����
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
            audioSource.Pause(); // ��ش���§���Ǥ���
        }

        // ��� Time.timeScale �� 0 ������ش��÷ӧҹ�ͧ�ء��觷������Ǣ�ͧ�Ѻ����
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