using UnityEngine;
using UnityEngine.SceneManagement;

public class SetScene : MonoBehaviour
{
    public void SetSceneClick(string scenename)
    {
        StaticClassForPassingData.sceneName = scenename; //ãªéã¹SceneController
        SceneManager.LoadScene("CaptureScene", LoadSceneMode.Additive);
    }

    public void SetCharacterClick(string charactername)
    {
        StaticClassForPassingData.characterName = charactername; //ãªéã¹CameraCapture
    }

    public void SetStoryClick(string storyname)
    {
        StaticClassForPassingData.storyName = storyname; //ãªéã¹SceneController
    }

    public void SetObjectName(string objectname)
    {
        StaticClassForPassingData.SelectObject = objectname; //ãªéã¹SceneController
    }
}
