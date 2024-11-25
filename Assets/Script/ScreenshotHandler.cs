using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.Android;


public class ScreenshotHandler : MonoBehaviour
{
    [Header("UI Objects Setting")]
    public List<GameObject> UIObjects;

    public void TakeScreenshot()
    {
        HideObjects();
        StartCoroutine(CaptureScreenshot());
        // WaitCapture();
        Invoke("ShowObjects", 2);

    }
    void SetStatusObjects(bool aStatus)
    {
        for (int i = 0; i < UIObjects.Count; i++)
        {
            UIObjects[i].SetActive(aStatus);
        }
    }
    void ShowObjects()
    {
        SetStatusObjects(true);
    }

    void HideObjects()
    {
        SetStatusObjects(false);
    }


    private IEnumerator CaptureScreenshot()

    {

        yield return new WaitForEndOfFrame();


        // Menyusun nama file

        string fileName = "screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";


        // Mengambil path direktori DCIM

        string dcimPath = GetDCIMPath();

        string filePath = dcimPath + '/' + fileName;
        Debug.Log(dcimPath);
        Debug.Log(filePath);


        // Mengambil screenshot

        ScreenCapture.CaptureScreenshot(fileName, 1);



        // Tunggu dua frame untuk memastikan screenshot disimpan

        yield return null; // Tunggu satu frame

        yield return null; // Tunggu frame kedua


        // Pindahkan file ke galeri

        string sourcePath = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(sourcePath))

        {

            File.Move(sourcePath, filePath);

            Debug.Log("Screenshot saved successfully at: " + filePath);

            // Beri tahu sistem untuk memindai file

            ScanFile(filePath);

        }

        else

        {

            Debug.LogError("Screenshot not found at: " + sourcePath);

        }

    }


    private string GetDCIMPath()

    {

        using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))

        {

            AndroidJavaObject dcimDirectory = environment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", new object[] { "DCIM" });

            return dcimDirectory.Call<string>("getAbsolutePath");

        }

    }


    private void ScanFile(string filePath)

    {

        using (AndroidJavaClass mediaScannerConnection = new AndroidJavaClass("android.media.MediaScannerConnection"))

        {

            AndroidJavaObject activity = new AndroidJavaClass("UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

            mediaScannerConnection.CallStatic("scanFile", activity, new string[] { filePath }, null, null);

        }

    }

}