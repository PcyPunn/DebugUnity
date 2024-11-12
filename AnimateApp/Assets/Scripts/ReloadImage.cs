using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReloadImage : MonoBehaviour
{
    public Button toggleButton;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleObjectInOtherScene);
    }

    void ToggleObjectInOtherScene()
    {
        // ���� GameObject ����ͧ���� Scene ���
        GameObject targetObject = GameObject.Find("TurtleRnT");

        if (targetObject != null)
        {
            // �Դ Object ��������� Coroutine ������ 1 �Թҷ�
            targetObject.SetActive(false);
            StartCoroutine(ReenableObjectAfterDelay(targetObject, 0.5f));
        }
        else
        {
            Debug.LogError("Target object not found in the other scene.");
        }
    }

    // Coroutine �����Դ Object �����������ѧ�ҡ���ҷ���˹�
    IEnumerator ReenableObjectAfterDelay(GameObject targetObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(true);
    }
}
