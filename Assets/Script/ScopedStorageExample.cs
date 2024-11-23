using UnityEngine;
using System;
using System.IO;
using UnityEngine.Android;

public class ScopedStorageExample : MonoBehaviour
{
    public Texture2D screenshotTexture;

    void Start()
    {
        // Pastikan memiliki izin untuk membaca media
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }

    public void SaveScreenshotToGallery()
    {
        // Ambil screenshot
        screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();

        if (screenshotTexture == null)
        {
            Debug.LogError("Screenshot capture failed!");
            return;
        }

        // Simpan ke MediaStore
        SaveImageToGallery(screenshotTexture, "Menjelajah_Negara", "Screenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");

        // Hapus texture setelah selesai
        Destroy(screenshotTexture);
    }

    void SaveImageToGallery(Texture2D texture, string albumName, string fileName)
    {
#if UNITY_ANDROID
        using (AndroidJavaClass mediaStore = new AndroidJavaClass("android.provider.MediaStore$Images$Media"))
        using (AndroidJavaObject contentResolver = GetActivity().Call<AndroidJavaObject>("getContentResolver"))
        {
            // Metadata file
            AndroidJavaObject values = new AndroidJavaObject("android.content.ContentValues");
            values.Call("put", "title", fileName);
            values.Call("put", "displayName", fileName);
            values.Call("put", "mime_type", "image/jpeg");
            values.Call("put", "relative_path", "DCIM/" + albumName);

            // URI file di MediaStore
            AndroidJavaObject uri = contentResolver.Call<AndroidJavaObject>("insert", mediaStore.GetStatic<AndroidJavaObject>("EXTERNAL_CONTENT_URI"), values);

            if (uri != null)
            {
                // Tulis data gambar ke output stream
                using (AndroidJavaObject outputStream = contentResolver.Call<AndroidJavaObject>("openOutputStream", uri))
                {
                    byte[] imageData = texture.EncodeToPNG();
                    outputStream.Call("write", imageData);
                    outputStream.Call("close");
                    File.WriteAllBytes(Application.dataPath + DateTime.Now.ToString("yyyyMMdd_HHmmss"), imageData);
                }

                Debug.Log("Image saved to gallery: " + fileName);
            }
            else
            {
                Debug.LogError("Failed to save image to MediaStore.");
            }
        }
#endif
    }

    AndroidJavaObject GetActivity()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}
