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
        // ค้นหา GameObject ที่ต้องการใน Scene อื่น
        GameObject targetObject = GameObject.Find("TurtleRnT");

        if (targetObject != null)
        {
            // ปิด Object แล้วเริ่ม Coroutine เพื่อรอ 1 วินาที
            targetObject.SetActive(false);
            StartCoroutine(ReenableObjectAfterDelay(targetObject, 0.5f));
        }
        else
        {
            Debug.LogError("Target object not found in the other scene.");
        }
    }

    // Coroutine ที่จะเปิด Object ขึ้นมาใหม่หลังจากเวลาที่กำหนด
    IEnumerator ReenableObjectAfterDelay(GameObject targetObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(true);
    }
}
