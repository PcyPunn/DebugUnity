using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject LoadingIcon;
    public GameObject LoadingText;
    public GameObject Loadingbg;

    void Start()
    {
        LoadingIcon.SetActive(false);
        LoadingText.SetActive(false);
        Loadingbg.SetActive(false);
    }

    public void ShowLoading()
    {
        LoadingIcon.SetActive(true);
        LoadingText.SetActive(true);
        Loadingbg.SetActive(true);
    }
}
