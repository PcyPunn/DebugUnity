using UnityEngine;
using UnityEngine.SceneManagement;

public class SetScene : MonoBehaviour
{
    public void SetSceneClick(string scenename)
    {
        StaticClassForPassingData.sceneName = scenename; //���SceneController
        SceneManager.LoadScene("CaptureScene", LoadSceneMode.Additive);
    }

    public void SetCharacterClick(string charactername)
    {
        StaticClassForPassingData.characterName = charactername; //���CameraCapture
    }

    public void SetStoryClick(string storyname)
    {
        StaticClassForPassingData.storyName = storyname; //���SceneController
    }

    public void SetObjectName(string objectname)
    {
        StaticClassForPassingData.SelectObject = objectname; //���SceneController
    }
}
