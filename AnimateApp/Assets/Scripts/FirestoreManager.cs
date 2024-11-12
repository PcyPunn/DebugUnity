using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FirestoreManager : MonoBehaviour
{
    public InputField dataInputField;
    public Button submitButton;

    private FirebaseAuth auth;
    private FirebaseUser user;
    private FirebaseFirestore db;

    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;

            // Check if the user is logged in
            user = auth.CurrentUser;
            if (user != null)
            {
                Debug.Log("User is logged in: " + user.UserId);
            }
            else
            {
                Debug.LogError("User is not logged in.");
                // Handle not logged in scenario
            }

            submitButton.onClick.AddListener(SubmitData);
        });
    }

    void SubmitData()
    {
        if (user != null)
        {
            string userInput = dataInputField.text;

            // Create a dictionary to store the data
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "userInput", userInput },
                { "timestamp", Timestamp.GetCurrentTimestamp() }
            };

            // Reference to the user's document in Firestore
            DocumentReference docRef = db.Collection(user.UserId).Document("Esop Key");

            // Set the data in Firestore
            docRef.SetAsync(data).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Data has been saved successfully.");
                }
                else
                {
                    Debug.LogError("Failed to save data: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("No user is logged in. Cannot submit data.");
        }
    }
}