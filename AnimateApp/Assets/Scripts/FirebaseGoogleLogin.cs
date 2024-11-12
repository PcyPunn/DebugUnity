using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UI;
using Google;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FirebaseGoogleLogin : MonoBehaviour
{
    public string GoogleWebAPI = "527819246949-4c4ron588befqm7796msclp8g16gqr53.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    FirebaseAuth auth;
    FirebaseUser user;

    void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleWebAPI,
            RequestIdToken = true,
            RequestEmail = true // ขอข้อมูลอีเมล
        };
        GoogleSignIn.Configuration = configuration;
    }

    void Start()
    {
        StaticClassForPassingData.checksession = "1";
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
            // เคลียร์การตั้งค่า GoogleSignIn เพื่อให้เลือกอีเมลใหม่
            GoogleSignIn.DefaultInstance.SignOut();
            user = null;
            auth.SignOut();
    
            Debug.Log("Please login");
        }
    }

    public void GoogleSignInClick()
    {
        Debug.Log("GoogleSignInClick called");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Google sign-in task faulted");
            foreach (var exception in task.Exception.InnerExceptions)
            {
                GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)exception;
                Debug.LogError("Error: " + error.Status + " - " + error.Message);
            }
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Google sign-in task canceled");
        }
        else
        {
            Debug.Log("Google sign-in successful");

            if (task.Result == null || string.IsNullOrEmpty(task.Result.IdToken))
            {
                Debug.LogError("GoogleSignInUser or IdToken is null");
                return;
            }

            Credential credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

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

                // เก็บข้อมูลผู้ใช้ใน PlayerPrefs
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