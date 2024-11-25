using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImageCapture : MonoBehaviour
{
    // [Header("Capture Setting")]
    // public string Path; // Tidak digunakan di Scoped Storage, tetapi bisa dibiarkan jika perlu.
    // public int Resolution = 1; // Skala resolusi.
    // private string filename;

    // [Header("UI Objects Setting")]
    // public List<GameObject> UIObjects;

    // [Header("After Capture Setting")]
    // public UnityEvent AfterCaptureEvent;

    // // Fungsi untuk menangkap screenshot
    // void WaitCapture()
    // {
    //     // Buat nama file berdasarkan timestamp
    //     filename = "Screenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";

    //     // Buat RenderTexture dengan ukuran layar
    //     RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

    //     // Tangkap screenshot ke RenderTexture
    //     ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);

    //     // Konversi RenderTexture menjadi Texture2D
    //     Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    //     RenderTexture.active = renderTexture;
    //     screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
    //     screenshot.Apply();
    //     RenderTexture.active = null;

    //     // Simpan screenshot ke folder Downloads menggunakan MediaStore
    //     SaveToDownloads(filename, screenshot.EncodeToJPG());

    //     // Hapus RenderTexture untuk mengosongkan memori
    //     RenderTexture.ReleaseTemporary(renderTexture);

    //     Debug.Log("Screenshot saved as: " + filename);
    // }

    // // Fungsi untuk menyimpan file ke folder Downloads
    // void SaveToDownloads(string filename, byte[] imageData)
    // {
    //     if (Application.platform == RuntimePlatform.Android)
    //     {
    //         using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    //         using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
    //         using (var resolver = currentActivity.Call<AndroidJavaObject>("getContentResolver"))
    //         using (var values = new AndroidJavaObject("android.content.ContentValues"))
    //         {
    //             // Tentukan metadata file
    //             values.Call("put", "DISPLAY_NAME", filename);
    //             values.Call("put", "MIME_TYPE", "image/jpeg");
    //             values.Call("put", "RELATIVE_PATH", "Download");

    //             // Masukkan file ke MediaStore
    //             var uri = resolver.Call<AndroidJavaObject>("insert", new AndroidJavaClass("android.provider.MediaStore$Images$Media").GetStatic<AndroidJavaObject>("EXTERNAL_CONTENT_URI"), values);
    //             using (var outputStream = resolver.Call<AndroidJavaObject>("openOutputStream", uri))
    //             {
    //                 outputStream.Call("write", imageData);
    //                 outputStream.Call("close");
    //             }
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogWarning("SaveToDownloads is only supported on Android.");
    //     }
    // }

    // // Tampilkan kembali UI Objects
    // void ShowObjects()
    // {
    //     SetStatusObjects(true);
    // }

    // // Sembunyikan UI Objects saat pengambilan gambar
    // void HideObjects()
    // {
    //     SetStatusObjects(false);
    // }

    // // Atur status UI Objects
    // void SetStatusObjects(bool status)
    // {
    //     for (int i = 0; i < UIObjects.Count; i++)
    //     {
    //         UIObjects[i].SetActive(status);
    //     }
    // }

    // // Fungsi untuk memulai pengambilan screenshot
    // public void InvokeCameraCapture()
    // {
    //     HideObjects(); // Sembunyikan UI
    //     WaitCapture(); // Tangkap screenshot
    //     Invoke("ShowObjects", 1); // Tampilkan UI setelah selesai
    //     AfterCaptureEvent.Invoke(); // Panggil event setelah selesai
    // }
}
