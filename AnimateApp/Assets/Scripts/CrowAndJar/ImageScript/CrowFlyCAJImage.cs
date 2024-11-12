using UnityEngine;
using UnityEngine.UI;

public class CrowFlyCAJImage : MonoBehaviour
{
    public static CrowFlyCAJImage Instance;

    public Image targetImage; // Image ของ GameObject หลัก
    public Material materialWithShader; // Material ที่มี Shader ที่ใช้ในการแทนที่พื้นหลัง

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("CrowFly Image script started.");

        if (targetImage != null)
        {
            Debug.Log("CrowFly Image is assigned.");
            if (StaticClassForPassingData.CrowFlyCAJTexture != null)
            {
                Debug.Log("Captured texture is not null.");
                // หมุนภาพก่อน
                Texture2D rotatedTexture = RotateTexture(StaticClassForPassingData.CrowFlyCAJTexture, 270);
                Debug.Log("Texture rotated.");
                // ตั้งค่า Material กับ Image
                SetMaterialWithTexture(targetImage, rotatedTexture);
                SaveTexture(rotatedTexture);
            }
            else
            {
                Debug.Log("Texture is Loaded.");
                Texture2D texture = LoadTexture();
                SetMaterialWithTexture(targetImage, texture);
            }
        }
        else
        {
            Debug.LogError("CrowFly Image is not assigned.");
        }
    }

    private void SetMaterialWithTexture(Image image, Texture2D texture)
    {
        if (materialWithShader == null)
        {
            Debug.LogError("Material with shader is not assigned.");
            return;
        }

        // สร้าง Material ใหม่จาก Material ที่มี Shader ที่ตั้งไว้
        Material newMaterial = new Material(materialWithShader);
        newMaterial.SetTexture("_BlendTex", texture);

        // ตั้งค่า Material ให้กับ Image
        image.material = newMaterial;
        Debug.Log("Material set with new texture.");
    }

    private Texture2D RotateTexture(Texture2D originalTexture, float angle)
    {
        int width = originalTexture.width;
        int height = originalTexture.height;
        Texture2D rotatedTexture = new Texture2D(height, width);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                rotatedTexture.SetPixel(height - y - 1, x, originalTexture.GetPixel(x, y));
            }
        }
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    private void SaveTexture(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        string encodedTexture = System.Convert.ToBase64String(bytes);
        PlayerPrefs.SetString("SavedCrowFlyImage", encodedTexture);
        PlayerPrefs.Save();
        Debug.Log("Texture saved to PlayerPrefs.");
    }

    private Texture2D LoadTexture()
    {
        string encodedTexture = PlayerPrefs.GetString("SavedCrowFlyImage", null);
        if (!string.IsNullOrEmpty(encodedTexture))
        {
            byte[] bytes = System.Convert.FromBase64String(encodedTexture);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }
        return null;
    }
}
