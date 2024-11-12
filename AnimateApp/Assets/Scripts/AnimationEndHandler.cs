using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEndHandler : MonoBehaviour
{
    // ���ͧ͢ Scene ���� unload
    public string sceneToUnload;

    // �ѧ��ѹ���ж١���¡����� Animation ��
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