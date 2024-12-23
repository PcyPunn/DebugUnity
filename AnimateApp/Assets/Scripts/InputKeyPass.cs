using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class InputKeyPass : MonoBehaviour
{
    public GameObject window;
    public GameObject inputarea;
    public GameObject qrarea;
    public GameObject statusBox;
    public TMP_Text errorMessage;
    public InputField dataInputField;



    void Start()
    {
        window.SetActive(false);
        qrarea.SetActive(false);
        inputarea.SetActive(false);
        statusBox.SetActive(false);
    }

    public void openWindow()
    {
        window.SetActive(true);
        errorMessage.text = "";
        dataInputField.text = "";
    }

    public void closeWindow()
    {
        window.SetActive(false);
        errorMessage.text = "";
        dataInputField.text = "";
    }

    public void openQRArea()
    {
        qrarea.SetActive(true);
    }

    public void closeQRArea()
    {
        qrarea.SetActive(false);
        StopCamera(); 
        Debug.Log("CloseQR");
    }

    public void closeStatusBox()
    {
        statusBox.SetActive(false);
        qrarea.SetActive(false);
    }

    public void openInputArea()
    {
        inputarea.SetActive(true);
    }

    public void closeInputArea()
    {
        inputarea.SetActive(false);
    }
    private void StopCamera()
{
    // ตรวจสอบว่าไม่มีการเรียกใช้กล้องหรือ library
    WebCamTexture webCam = qrarea.GetComponentInChildren<RawImage>().texture as WebCamTexture;
    if (webCam != null && webCam.isPlaying)
    {
        webCam.Stop(); // หยุดการทำงานของกล้อง
    }
}
}
