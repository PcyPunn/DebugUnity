using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectVisibilityController : MonoBehaviour
{
    public List<GameObject> targetObjects; // ��¡���ѵ�ط���ͧ��äǺ�������ʴ���
    private Coroutine hideCoroutine;

    void Start()
    {
        // ��͹ Renderer �ͧ�ѵ������������ Scene
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                SetVisibility(targetObject, false);
            }
        }
    }

    void Update()
    {
        // ��Ǩ�Ѻ��ä�ԡ���͡��������˹�Ҩ�
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            // �ʴ� Renderer �ͧ�ѵ�ط������������� Coroutine ����Ѻ��͹�ѵ����ѧ�ҡ 3 �Թҷ�
            foreach (GameObject targetObject in targetObjects)
            {
                if (targetObject != null)
                {
                    SetVisibility(targetObject, true);
                }
            }
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HideObjectsAfterDelay(3.0f));
        }
    }

    IEnumerator HideObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                SetVisibility(targetObject, false);
            }
        }
    }

    void SetVisibility(GameObject targetObject, bool isActive)
    {
        // ����Ѻ Image component
        Image[] images = targetObject.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            image.enabled = isActive;
        }

        // ����Ѻ Sprite Renderer component
        SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = isActive;
        }
    }
}