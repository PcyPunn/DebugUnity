using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeVertical : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 1; // เปลี่ยนค่าเริ่มต้นให้เป็น 1 (บนสุด)
    float[] pos;

    void Start()
    {
        int childCount = transform.childCount;
        pos = new float[childCount];
        float distance = 1f / (childCount - 1f);

        for (int i = 0; i < childCount; i++)
        {
            pos[i] = 1f - distance * i; // เปลี่ยนการคำนวณเพื่อให้ Object อันบนสุดมีค่าเป็น 1 และอันล่างสุดเป็น 0
        }
    }

    void Update()
    {
        float distance = 1f / (pos.Length - 1f);

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], Time.deltaTime * 10f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                if (i < transform.childCount)
                {
                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), Time.deltaTime * 10f);
                }
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        if (j < transform.childCount)
                        {
                            transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), Time.deltaTime * 10f);
                        }
                    }
                }
            }
        }
    }
}