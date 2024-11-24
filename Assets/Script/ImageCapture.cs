using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Android;

public class ImageCapture : MonoBehaviour
{
    [Header("Capture Setting")]
    public string FolderName = "Menjelajah_Negara"; // Nama folder di direktori publik
    public int Resolution;
    string filename;

    [Header("UI Objects Setting")]
    public List<GameObject> UIObjects;

    [Header("After Capture Setting")]
    public UnityEvent AfterCaptureEvent;


    void WaitCapture()
    {
        string directoryPath = "/storage/emulated/0/DCIM/Screenshots/Menjelajah_Negara";

        // Pastikan folder ada atau buat jika tidak ada
        // if (!Directory.Exists(directoryPath))
        // {
        //     Directory.CreateDirectory(directoryPath);
        // }

        filename = directoryPath + "/" + DateTime.Now.ToString("MM_dd_yyyy_h_mm_ss") + ".png";
        ScreenCapture.CaptureScreenshot(filename, Resolution);
        Debug.Log("Screenshot saved to: " + filename);
    }

    string GetSavePath()
    {
        string savePath;

#if UNITY_ANDROID
        savePath = Path.Combine(Application.persistentDataPath, FolderName);

        // Untuk galeri Google Photos, simpan di folder Pictures atau DCIM
        savePath = Path.Combine("/storage/emulated/0/DCIM/Screenshots", FolderName);
#elif UNITY_IOS
        savePath = Path.Combine(Application.persistentDataPath, FolderName);
#else
        savePath = Path.Combine(Application.dataPath, FolderName);
#endif

        return savePath;
    }

    void ShowObjects()
    {
        SetStatusObjects(true);
    }

    void HideObjects()
    {
        SetStatusObjects(false);
    }

    void SetStatusObjects(bool aStatus)
    {
        for (int i = 0; i < UIObjects.Count; i++)
        {
            UIObjects[i].SetActive(aStatus);
        }
    }

    public void InvokeCameraCapture()
    {
        HideObjects();
        WaitCapture();
        Invoke("ShowObjects", 1);
        AfterCaptureEvent.Invoke();
    }
}

