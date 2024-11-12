using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeHorizon : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    float[] pos;

    void Start()
    {
        int childCount = transform.childCount;
        pos = new float[childCount];
        float distance = 1f / (childCount - 1f);

        for (int i = 0; i < childCount; i++)
        {
            pos[i] = distance * i;
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
                    Transform child = transform.GetChild(i);
                    child.localScale = Vector2.Lerp(child.localScale, new Vector2(1f, 1f), Time.deltaTime * 10f);

                    // รักษาขนาดของ Text ภายใน Button
                    foreach (Text text in child.GetComponentsInChildren<Text>())
                    {
                        text.transform.localScale = Vector3.one;
                    }
                }
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        if (j < transform.childCount)
                        {
                            Transform child = transform.GetChild(j);
                            child.localScale = Vector2.Lerp(child.localScale, new Vector2(0.8f, 0.8f), Time.deltaTime * 10f);

                            // รักษาขนาดของ Text ภายใน Button
                            foreach (Text text in child.GetComponentsInChildren<Text>())
                            {
                                text.transform.localScale = Vector3.one;
                            }
                        }
                    }
                }
            }
        }
    }
}