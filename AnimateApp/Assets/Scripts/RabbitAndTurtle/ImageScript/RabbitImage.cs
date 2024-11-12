using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RabbitImage : MonoBehaviour
{
    public static RabbitImage Instance;

    public Image targetImage;
    public Material materialWithShader;

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
        if (StaticClassForPassingData.RabbitTexture != null)
        {
            StartCoroutine(HandleTextureSetup(StaticClassForPassingData.RabbitTexture));
        }
        else
        {
            Texture2D texture = LoadTexture();
            SetMaterialWithTexture(targetImage, texture);
        }
    }

    IEnumerator HandleTextureSetup(Texture2D originalTexture)
    {
        Texture2D rotatedTexture = RotateTexture(originalTexture, 270);

        SetMaterialWithTexture(targetImage, rotatedTexture);
        SaveTexture(rotatedTexture);

        yield return null;
    }

    private void SetMaterialWithTexture(Image image, Texture2D texture)
    {
        Material newMaterial = new Material(materialWithShader);
        newMaterial.SetTexture("_BlendTex", texture);

        image.material = newMaterial;
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
        PlayerPrefs.SetString("SavedRabbitRnT", encodedTexture);
        PlayerPrefs.Save();
    }

    private Texture2D LoadTexture()
    {
        string encodedTexture = PlayerPrefs.GetString("SavedRabbitRnT", null);
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
