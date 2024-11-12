using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI; // ����Ѻ�Ǻ��� UI

public class AudioRecordController : MonoBehaviour
{
    // �� Animator ��� AudioSource � List
    private List<Animator> animators = new List<Animator>();

    public float stopTime = 10f; // ������Թҷշ���ͧ�����ش  
    public Button recordButton; // ���� Pause/Resume
    public GameObject timeRecord;
    public GameObject lineRecord;
    public GameObject bgRecord;
    public AudioSource audioSource;       // AudioSource ����Ѻ������§���ѹ�֡
    private AudioClip recordedClip;       // �����§���ѹ�֡


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

        // ��͹���� Pause �͹�������
        recordButton.gameObject.SetActive(false);
        lineRecord.gameObject.SetActive(false);
        bgRecord.gameObject.SetActive(false);

        // ����������Ѻ�ѧ��ѹ
        recordButton.onClick.AddListener(StartRecording);

        // ������Ѻ����
        StartCoroutine(StopAllAfterTime(stopTime));
    }

    IEnumerator StopAllAfterTime(float time)
    {
        // �������� 'time' �Թҷ�
        yield return new WaitForSeconds(time);

        // ��ش��÷ӧҹ�ͧ Animator ��� AudioSource ������
        foreach (Animator animator in animators)
        {
            animator.speed = 0;
        }

        // �ʴ����� Pause/Resume
        recordButton.gameObject.SetActive(true);
        lineRecord.gameObject.SetActive(true);
        bgRecord.gameObject.SetActive(true);
    }

    // �ѧ��ѹ�Դ/�Դ Animator ��� AudioSource ����͡�����
    void PlayAnimation()
    {
        Debug.Log("Animation is Playing.");
        Microphone.End(null);  // ��ش�ѹ�֡
        recordButton.gameObject.SetActive(false);  // ��͹�����ѹ�֡
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
        if (Microphone.devices.Length == 0)  // ��Ǩ�ͺ���������⿹��ػ�ó��������
        {
            Debug.LogError("No microphone devices found.");
            return;
        }

        Debug.Log("Audio is Recording.");
        // ������ѹ�֡���§ (�٧�ش 8 �Թҷ� ��������� 44100 Hz)
        recordedClip = Microphone.Start(null, false, 8, 44100);
        timeRecord.gameObject.SetActive(true);

        if (recordedClip == null)  // ��Ǩ�ͺ��ҡ�úѹ�֡������������
        {
            Debug.LogError("Failed to start recording.");
            return;
        }

        Invoke("PlayAnimation", 8);  // ��ش�ѹ�֡�ѵ��ѵ���ѧ 8 �Թҷ�
    }

    void PlayRecordedAudio()
    {
        Debug.Log("Audio is Playing.");
        audioSource.clip = recordedClip;  // ������§���ѹ�֡� Aud ioSource
        audioSource.Play();  // ������§
    }
}