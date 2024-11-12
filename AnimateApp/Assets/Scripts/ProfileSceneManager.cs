using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Google;

public class ProfileSceneManager : MonoBehaviour
{
    public GameObject panel;
    public Button openButton;
    public Button closeButton;

    public Button LogoutButton;
    public Text UsernameText;
    public Text UserEmailText;
    public Image UserProfileImage;
    FirebaseAuth auth;

    void Start()
    {
        if (panel == null || openButton == null || closeButton == null)
        {
            Debug.LogError("Please assign all references in the Inspector.");
            return;
        }

        panel.SetActive(false);

        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);

        // �֧�����ż����ҡ PlayerPrefs
        string username = PlayerPrefs.GetString("Username", "No Name");
        string userEmail = PlayerPrefs.GetString("UserEmail", "No Email");
        string userProfilePicUrl = PlayerPrefs.GetString("UserProfilePic", "");

        // �ʴ�������� UI
        UsernameText.text = username;
        UserEmailText.text = userEmail;

        Debug.Log($"Loaded UserName: {username}, UserEmail: {userEmail}, UserProfilePic: {userProfilePicUrl}");

        // ��Ŵ����ʴ��ٻ�Ҿ�����
        if (!string.IsNullOrEmpty(userProfilePicUrl))
        {
            StartCoroutine(LoadImage(userProfilePicUrl));
        }

        // ������� FirebaseAuth
        auth = FirebaseAuth.DefaultInstance;

        // �������Ϳѧ��ѹ Logout �Ѻ����
        LogoutButton.onClick.AddListener(Logout);
    }

    IEnumerator LoadImage(string imageUrl)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                UserProfileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            }
        }
    }

    void Logout()
    {
        // ��͡��ҷ�����ҡ Firebase
        auth.SignOut();

        PlayerPrefs.SetString("Session", "0");
        PlayerPrefs.SetString("Username", "No Name");
        PlayerPrefs.SetString("UserEmail", "No Email");
        PlayerPrefs.Save();

        // �������õ�駤�� GoogleSignIn ����������͡���������
        GoogleSignIn.DefaultInstance.SignOut();

        // ��Ŵ Scene �ͧ˹�Ҩ���͡�Թ���� (�������� Scene ���� "LoginScene")
        SceneManager.LoadScene("LoginScene");
    }

    void OpenPanel()
    {
        panel.SetActive(true);
    }

    void ClosePanel()
    {
        panel.SetActive(false);
    }
}