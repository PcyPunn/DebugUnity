using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Facebook.Unity;

public class LogoutProfileManager : MonoBehaviour
{
    public GameObject panel;
    public Button openButton;
    public Button closeButton;
    public Button logoutButton;
    // public Text usernameText;
    public Text userEmailText;
    // public Image userProfileImage;
    public Text FB_userName; // เพิ่มการอ้างอิงสำหรับแสดงชื่อ Facebook
    public Image FB_profilePic; // เพิ่มการอ้างอิงสำหรับแสดงรูปโปรไฟล์ Facebook
    private FirebaseAuth auth;

    void Start()
    {
        // ตรวจสอบการกำหนดค่าของ reference
        // if (panel == null || openButton == null || closeButton == null || logoutButton == null || usernameText == null || userEmailText == null || userProfileImage == null || FB_userName == null || FB_profilePic == null)
        // {
        //     Debug.LogError("Please assign all references in the Inspector.");
        //     return;
        // }

        // ซ่อน panel เริ่มต้น
        panel.SetActive(false);

        // ตั้งค่า listener สำหรับปุ่ม
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
        logoutButton.onClick.AddListener(Logout);

        // โหลดข้อมูลผู้ใช้จาก PlayerPrefs
        LoadUserData();

        // เริ่มต้น FirebaseAuth
        auth = FirebaseAuth.DefaultInstance;

        // ตรวจสอบการเข้าสู่ระบบ Facebook
        DealWithFbMenus(FB.IsLoggedIn);
    }

    void LoadUserData()
    {
        // โหลดข้อมูลผู้ใช้จาก PlayerPrefs
        string username = PlayerPrefs.GetString("Username", "No Name");
        string userEmail = PlayerPrefs.GetString("UserEmail", "No Email");
        string userProfilePicUrl = PlayerPrefs.GetString("UserProfilePic", "");

        // แสดงข้อมูลที่โหลดใน UI
        // usernameText.text = username;
        userEmailText.text = userEmail;

        Debug.Log($"Loaded UserName: {username}, UserEmail: {userEmail}, UserProfilePic: {userProfilePicUrl}");

        // โหลดรูปโปรไฟล์ถ้ามี URL
        if (!string.IsNullOrEmpty(userProfilePicUrl))
        {
            StartCoroutine(LoadImage(userProfilePicUrl));
        }
    }

    void DealWithFbMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name,last_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {
            Debug.Log("Not logged in");
        }
    }

    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            string firstName = result.ResultDictionary.ContainsKey("first_name") ? result.ResultDictionary["first_name"].ToString() : "";
            string lastName = result.ResultDictionary.ContainsKey("last_name") ? result.ResultDictionary["last_name"].ToString() : "";
            string name = firstName + " " + lastName;
            if (FB_userName != null)
                FB_userName.text = name;

            Debug.Log("" + name);
        }
        else
        {
            Debug.Log(result.Error);
        }
        panel.SetActive(true); // เปิด panel แทนการเปลี่ยนการแสดงผล
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            Debug.Log("Profile Pic");
            if (FB_profilePic != null) FB_profilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.Log(result.Error);
        }
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
                // userProfileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }

    void Logout()
    {
        // ลงชื่อออกจาก Firebase
        auth.SignOut();

        // ลบข้อมูลผู้ใช้จาก PlayerPrefs
        PlayerPrefs.SetString("Session", "0");
        PlayerPrefs.SetString("Username", "No Name");
        PlayerPrefs.SetString("UserEmail", "No Email");
        PlayerPrefs.Save();

        // ลงชื่อออกจาก Facebook
        if (FB.IsLoggedIn)
        {
            FB.LogOut();
        }

        // โหลดฉาก Login
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
