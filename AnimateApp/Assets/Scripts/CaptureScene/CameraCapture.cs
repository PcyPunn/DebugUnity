using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class CameraCapture : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public Button captureButton;
    public RawImage cameraView;

    void OnEnable()
    {
        captureButton.onClick.AddListener(CapturePhoto);
        arCameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnDisable()
    {
        arCameraManager.frameReceived -= OnCameraFrameReceived;
        captureButton.onClick.RemoveListener(CapturePhoto);
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if (arCameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            try
            {
                var conversionParams = new XRCpuImage.ConversionParams
                {
                    inputRect = new RectInt(0, 0, image.width, image.height),
                    outputDimensions = new Vector2Int(image.width, image.height),
                    outputFormat = TextureFormat.RGBA32,
                    transformation = XRCpuImage.Transformation.MirrorX
                };

                var texture = new Texture2D(image.width, image.height, TextureFormat.RGBA32, false);
                var rawTextureData = texture.GetRawTextureData<byte>();
                image.Convert(conversionParams, rawTextureData);
                texture.Apply();

                //Texture2D rotatedTexture = RotateTexture(texture, 90);
                cameraView.texture = texture;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error processing camera frame: {ex.Message}");
            }
            finally
            {
                image.Dispose();
            }
        }
        else
        {
            Debug.LogError("Failed to acquire latest CPU image.");
        }
    }

    public void CapturePhoto()
    {
        if (arCameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.RGBA32,
                transformation = XRCpuImage.Transformation.MirrorX
            };

            var texture = new Texture2D(image.width, image.height, TextureFormat.RGBA32, false);
            var rawTextureData = texture.GetRawTextureData<byte>();
            image.Convert(conversionParams, rawTextureData);
            texture.Apply();

            Texture2D rotatedTexture = RotateTexture(texture, 0);

            //DuckSongScene
            if (StaticClassForPassingData.characterName == "DuckDuckSong")
            {
                StaticClassForPassingData.DuckTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "FishDuckSong")
            {
                StaticClassForPassingData.FishTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "CrabDuckSong")
            {
                StaticClassForPassingData.CrabTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "ShellDuckSong")
            {
                StaticClassForPassingData.ShellTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "Tree1DuckSong")
            {
                StaticClassForPassingData.Tree1Texture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "Tree2DuckSong")
            {
                StaticClassForPassingData.Tree2Texture = rotatedTexture;
            }

            //CrowAndJar
            if (StaticClassForPassingData.characterName == "CrowFlyCAJ")
            {
                StaticClassForPassingData.CrowFlyCAJTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "CrowWalkCAJ")
            {
                StaticClassForPassingData.CrowWalkCAJTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "JarCAJ")
            {
                StaticClassForPassingData.JarCAJTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "StonesCAJ")
            {
                StaticClassForPassingData.StonesCAJTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "ForestCAJ")
            {
                StaticClassForPassingData.ForestCAJTexture = rotatedTexture;
            }

            //ThreePigs
            if (StaticClassForPassingData.characterName == "Pig1")
            {
                StaticClassForPassingData.Pig1Texture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "Pig2")
            {
                StaticClassForPassingData.Pig2Texture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "Pig3")
            {
                StaticClassForPassingData.Pig3Texture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "PigMom")
            {
                StaticClassForPassingData.PigMomTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "Wolf")
            {
                StaticClassForPassingData.WolfTexture = rotatedTexture;
            }

            //RabbitAndTurtle
            if (StaticClassForPassingData.characterName == "RabbitRnT")
            {
                StaticClassForPassingData.RabbitTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "TurtleRnT")
            {
                StaticClassForPassingData.TurtleTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "RabbitSleepRnT")
            {
                StaticClassForPassingData.RabbitSleepTexture = rotatedTexture;
            }
            else if (StaticClassForPassingData.characterName == "RabbitWorryRnT")
            {
                StaticClassForPassingData.RabbitWorryTexture = rotatedTexture;
            }

            image.Dispose();

        }
    }

    Texture2D RotateTexture(Texture2D originalTexture, float angle)
    {
        int width = originalTexture.width;
        int height = originalTexture.height;
        Texture2D rotatedTexture = new Texture2D(height, width);
    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int newX = y;
                int newY = width - x - 1;
                rotatedTexture.SetPixel(newX, newY, originalTexture.GetPixel(x, y));
            }
        }
    
        rotatedTexture.Apply();
        return rotatedTexture;
    }
}