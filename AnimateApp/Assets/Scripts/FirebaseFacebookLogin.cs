using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using UnityEngine.UI;
using Firebase.Extensions;

public class FirebaseFacebookLogin : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.LogError("Failed to initialize Facebook SDK");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void Start()
    {
        InitFirebase();
        CheckLoginStatus();
    }

    void InitFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    void CheckLoginStatus()
    {
        string session = PlayerPrefs.GetString("Session", "1");
        Debug.Log("Check login is starting...");

        if (StaticClassForPassingData.checksession == session)
        {
            Debug.Log("User is already logged in, redirecting to Main...");
            SceneManager.LoadScene("Homepage");
        }
        else
        {
            auth.SignOut();
            user = null;

            Debug.Log("Please login");
        }
    }

    public void FacebookSignInClick()
    {
        var permissions = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permissions, OnFacebookAuthenticatedFinished);
    }

    void OnFacebookAuthenticatedFinished(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Facebook sign-in successful");

            AccessToken token = AccessToken.CurrentAccessToken;
            Firebase.Auth.Credential credential = FacebookAuthProvider.GetCredential(token.TokenString);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(authTask =>
            {
                if (authTask.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (authTask.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + authTask.Exception);
                    return;
                }

                Debug.Log("Firebase sign-in successful");

                user = auth.CurrentUser;

                if (user != null)
                {
                    string displayName = user.DisplayName ?? "No Name";
                    string email = user.Email ?? "No Email";
                    string photoUrl = user.PhotoUrl?.ToString() ?? "";

                    PlayerPrefs.SetString("Username", displayName);
                    PlayerPrefs.SetString("UserEmail", email);
                    PlayerPrefs.SetString("UserProfilePic", CheckImageUrl(photoUrl));
                    PlayerPrefs.SetString("Session", "1");
                    PlayerPrefs.Save();

                    Debug.Log($"Saved UserName: {displayName}, UserEmail: {email}, UserProfilePic: {photoUrl}");

                    SceneManager.LoadScene("Homepage");
                }
                else
                {
                    Debug.LogError("User is null after sign-in.");
                    return;
                }
            });
        }
        else
        {
            Debug.LogError("Facebook sign-in canceled or failed: " + result.Error);
        }
    }

    private string CheckImageUrl(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            return url;
        }

        return "";
    }
}
