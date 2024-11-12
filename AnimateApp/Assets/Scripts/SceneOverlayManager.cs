using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOverlayManager : MonoBehaviour
{
    // ชื่อของ Scene ที่จะโหลดแบบ Additive
    public string overlaySceneName;

    // ฟังก์ชันในการโหลด Scene แบบ Additive
    public void LoadOverlayScene()
    {
        if (!string.IsNullOrEmpty(overlaySceneName))
        {
            SceneManager.LoadSceneAsync(overlaySceneName, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogWarning("Overlay scene name is not set");
        }
    }

    // ฟังก์ชันในการลบ Scene ที่ถูกโหลดแบบ Additive
    public void UnloadOverlayScene()
    {
        if (!string.IsNullOrEmpty(overlaySceneName))
        {
            SceneManager.UnloadSceneAsync(overlaySceneName);
        }
        else
        {
            Debug.LogWarning("Overlay scene name is not set");
        }
    }
}