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

        // ดึงข้อมูลผู้ใช้จาก PlayerPrefs
        string username = PlayerPrefs.GetString("Username", "No Name");
        string userEmail = PlayerPrefs.GetString("UserEmail", "No Email");
        string userProfilePicUrl = PlayerPrefs.GetString("UserProfilePic", "");

        // แสดงข้อมูลใน UI
        UsernameText.text = username;
        UserEmailText.text = userEmail;

        Debug.Log($"Loaded UserName: {username}, UserEmail: {userEmail}, UserProfilePic: {userProfilePicUrl}");

        // โหลดและแสดงรูปภาพผู้ใช้
        if (!string.IsNullOrEmpty(userProfilePicUrl))
        {
            StartCoroutine(LoadImage(userProfilePicUrl));
        }

        // เริ่มต้น FirebaseAuth
        auth = FirebaseAuth.DefaultInstance;

        // เชื่อมต่อฟังก์ชัน Logout กับปุ่ม
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
        // ล็อกเอาท์ผู้ใช้จาก Firebase
        auth.SignOut();

        PlayerPrefs.SetString("Session", "0");
        PlayerPrefs.SetString("Username", "No Name");
        PlayerPrefs.SetString("UserEmail", "No Email");
        PlayerPrefs.Save();

        // เคลียร์การตั้งค่า GoogleSignIn เพื่อให้เลือกอีเมลใหม่
        GoogleSignIn.DefaultInstance.SignOut();

        // โหลด Scene ของหน้าจอล็อกอินใหม่ (สมมติว่า Scene ชื่อ "LoginScene")
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