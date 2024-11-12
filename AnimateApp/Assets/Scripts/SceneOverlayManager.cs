using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOverlayManager : MonoBehaviour
{
    // ���ͧ͢ Scene ������ŴẺ Additive
    public string overlaySceneName;

    // �ѧ��ѹ㹡����Ŵ Scene Ẻ Additive
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

    // �ѧ��ѹ㹡��ź Scene ���١��ŴẺ Additive
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