using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectVisibilityController : MonoBehaviour
{
    public List<GameObject> targetObjects; // รายการวัตถุที่ต้องการควบคุมการแสดงผล
    private Coroutine hideCoroutine;

    void Start()
    {
        // ซ่อน Renderer ของวัตถุเมื่อเริ่ม Scene
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
        // ตรวจจับการคลิกหรือการสัมผัสหน้าจอ
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            // แสดง Renderer ของวัตถุทั้งหมดและเริ่ม Coroutine สำหรับซ่อนวัตถุหลังจาก 3 วินาที
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
        // สำหรับ Image component
        Image[] images = targetObject.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            image.enabled = isActive;
        }

        // สำหรับ Sprite Renderer component
        SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = isActive;
        }
    }
}