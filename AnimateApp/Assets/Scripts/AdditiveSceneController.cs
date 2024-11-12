using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneController : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public string[] targetSceneNames;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive && System.Array.Exists(targetSceneNames, name => name == scene.name))
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}