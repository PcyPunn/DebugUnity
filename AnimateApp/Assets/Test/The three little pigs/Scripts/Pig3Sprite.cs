using UnityEngine;
using System.Collections;

public class Pig3Sprite : MonoBehaviour
{
    public static Pig3Sprite Instance;

    public SpriteRenderer targetSpriteRenderer;
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
        Debug.Log("Pig3 script started.");

        if (targetSpriteRenderer != null)
        {
            Debug.Log("Pig3 SpriteRenderer is assigned.");
            if (StaticClassForPassingData.Pig3Texture != null)
            {
                Debug.Log("Captured texture is not null.");
                // ��ع�Ҿ��͹
                Texture2D rotatedTexture = RotateTexture(StaticClassForPassingData.Pig3Texture, 270);
                Debug.Log("Texture rotated.");
                // ��駤�� Material �Ѻ SpriteRenderer
                SetMaterialWithTexture(targetSpriteRenderer, rotatedTexture);
                SaveTexture(rotatedTexture);
            }
            else
            {
                Debug.Log("Texture is Loaded.");
                Texture2D texture = LoadTexture();
                SetMaterialWithTexture(targetSpriteRenderer, texture);
            }
        }
        else
        {
            Debug.LogError("Pig3 SpriteRenderer is not assigned.");
        }
    }

    private void SetMaterialWithTexture(SpriteRenderer spriteRenderer, Texture2D texture)
    {
        if (materialWithShader == null)
        {
            Debug.LogError("Material with shader is not assigned.");
            return;
        }

        // ���ҧ Material ����ҡ Material ����� Shader ��������
        Material newMaterial = new Material(materialWithShader);
        newMaterial.SetTexture("_BlendTex", texture);

        // ��駤�� Material ���Ѻ SpriteRenderer
        spriteRenderer.material = newMaterial;
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
        PlayerPrefs.SetString("SavedPig3", encodedTexture);
        PlayerPrefs.Save();
        Debug.Log("Texture saved to PlayerPrefs.");
    }

    private Texture2D LoadTexture()
    {
        string encodedTexture = PlayerPrefs.GetString("SavedPig3", null);
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