using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(StaticClassForPassingData.sceneName, LoadSceneMode.Additive);
        
        //SceneManager.UnloadSceneAsync(StaticClassForPassingData.storyName);
        //SceneManager.LoadScene(StaticClassForPassingData.storyName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("CaptureScene");
    }
}
