using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEndHandler : MonoBehaviour
{
    // ชื่อของ Scene ที่จะ unload
    public string sceneToUnload;

    // ฟังก์ชันที่จะถูกเรียกเมื่อ Animation จบ
    public void OnAnimationEnd()
    {
        if (!string.IsNullOrEmpty(sceneToUnload))
        {
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }
        else
        {
            Debug.LogWarning("Scene name to unload is not set.");
        }
    }
}