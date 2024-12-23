using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using TMPro;

public class QRCodeScanner : MonoBehaviour
{
    public RawImage cameraView;
    public TMP_Text statusText;
    public GameObject statusBox;

    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public GameObject story4;
    public GameObject story5;
    public GameObject story6;

    private WebCamTexture webcamTexture;
    private BarcodeReader barcodeReader;
    private FirebaseAuth auth;
    private FirebaseUser user;
    private FirebaseFirestore db;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                db = FirebaseFirestore.DefaultInstance;

                user = auth.CurrentUser;
                if (user != null)
                {
                    Debug.Log("User logged in: " + user.UserId);
                }
                else
                {
                    Debug.LogError("No user is logged in.");
                }
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies.");
            }
        });

        InitializeCamera();
        InitializeBarcodeReader();
    }

    void Update()
    {
        if (webcamTexture != null && webcamTexture.isPlaying && webcamTexture.width > 100)
        {
            try
            {
                var data = barcodeReader.Decode(webcamTexture.GetPixels32(), webcamTexture.width, webcamTexture.height);
                if (data != null)
                {
                    Debug.Log("QR Code Content: " + data.Text);
                    //webcamTexture.Stop(); // ��ش���ͧ���Ǥ���������᡹�����
                    cameraView.texture = null;
                    cameraView.material.mainTexture = null;
                    webcamTexture = null;
                    ProcessQRCodeData(data.Text);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error decoding QR Code: " + ex.Message);
            }
        }
    }

    void InitializeCamera()
    {
        if (cameraView != null)
        {
            webcamTexture = new WebCamTexture();
            cameraView.texture = webcamTexture;
            cameraView.material.mainTexture = webcamTexture;
            Debug.Log("Camera initialized.");
            webcamTexture.Play();
        }
    }

    void InitializeBarcodeReader()
    {
        barcodeReader = new BarcodeReader
        {
            AutoRotate = true,
            TryInverted = true,
            Options = new DecodingOptions
            {
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                TryHarder = true
            }
        };
    }

    void ProcessQRCodeData(string qrData)
    {
        if (user != null)
        {
            string collection = "keypass";
            string status = "used";

            DocumentReference docRef = db.Collection(collection).Document(qrData);

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        Debug.Log("Document found: " + qrData);

                        if (snapshot.ContainsField("User UID") && snapshot.GetValue<string>("User UID") != null)
                        {
                            statusBox.SetActive(true);
                            statusText.text = "The key is used!";
                            statusText.color = Color.red;
                            Debug.LogWarning("The key is used.");
                        }
                        else
                        {
                            Dictionary<string, object> updateData = new Dictionary<string, object>
                            {
                                { "User UID", user.UserId },
                                { "Status", status },
                                { "timestamp", Timestamp.GetCurrentTimestamp() }
                            };

                            docRef.UpdateAsync(updateData).ContinueWithOnMainThread(updateTask =>
                            {
                                if (updateTask.IsCompleted)
                                {
                                    statusBox.SetActive(true);
                                    statusText.text = "Successfully.";
                                    statusText.color = Color.green;
                                    Debug.Log("User UID has been saved successfully in the document.");

                                    if (snapshot.ContainsField("Open"))
                                    {
                                        List<object> openArray = snapshot.GetValue<List<object>>("Open");

                                        foreach (object item in openArray)
                                        {
                                            string objectName = item.ToString();
                                            if (objectName == "Story1" && story1 != null) story1.SetActive(true);
                                            else if (objectName == "Story2" && story2 != null) story2.SetActive(true);
                                            else if (objectName == "Story3" && story3 != null) story3.SetActive(true);
                                            else if (objectName == "Story4" && story4 != null) story4.SetActive(true);
                                            else if (objectName == "Story5" && story5 != null) story5.SetActive(true);
                                            else if (objectName == "Story6" && story6 != null) story6.SetActive(true);
                                            else
                                            {
                                                Debug.LogWarning("Object not found or reference not set for: " + objectName);
                                            }
                                        }

                                        SaveOpenFieldToUsersCollection(openArray);
                                    }
                                    else
                                    {
                                        Debug.LogWarning("Field 'Open' does not exist in document.");
                                    }
                                }
                                else
                                {
                                    Debug.LogError("Failed to update document: " + updateTask.Exception);
                                }
                            });
                        }
                    }
                    else
                    {
                        statusBox.SetActive(true);
                        statusText.text = "Unknown the key!";
                        statusText.color = Color.red;
                        Debug.LogWarning("Unknown the key.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to retrieve document: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("No user is logged in. Cannot submit data.");
        }
    }

    private void SaveOpenFieldToUsersCollection(List<object> openArray)
    {
        DocumentReference userDocRef = db.Collection("users").Document(user.UserId);

        Dictionary<string, object> updateData = new Dictionary<string, object>
        {
            { "Open", openArray }
        };

        userDocRef.UpdateAsync(updateData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Field 'Open' has been updated successfully in 'users' collection.");
            }
            else
            {
                Debug.LogError("Failed to update 'Open' field in 'users' collection: " + task.Exception);
            }
        });

        string openData = string.Join(",", openArray);
        PlayerPrefs.SetString("OpenObjects", openData);
        PlayerPrefs.Save();
        Debug.Log("Object states saved locally.");
    }

    public void ActiveScanner()
    {
        InitializeCamera();
        InitializeBarcodeReader();
    }
}