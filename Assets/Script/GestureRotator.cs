using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRotator : MonoBehaviour
{
    [Header("Main Settings")]
    public bool RotatorEnabled;

    [Header("Movement Settings")]
    public float rotationSpeed = 1.0f; // Kecepatan rotasi objek
    private bool isRotating = false; // Apakah objek sedang dalam proses rotasi
    private Vector2 lastTouchPosition; // Posisi sentuhan sebelumnya

    void Update()
    {
        // Mendeteksi input sentuhan
        if (RotatorEnabled && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Menyimpan posisi sentuhan saat ini sebagai posisi sebelumnya
                    lastTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Menghitung perubahan posisi sentuhan
                    float deltaPositionX = touch.position.x - lastTouchPosition.x;

                    // Menghitung rotasi berdasarkan perubahan posisi
                    float rotation = deltaPositionX * -rotationSpeed;

                    // Memutar objek horizontal
                    transform.Rotate(Vector3.up, rotation);

                    // Memperbarui posisi sentuhan terakhir
                    lastTouchPosition = touch.position;

                    // Objek sedang dalam proses rotasi
                    isRotating = true;
                    break;

                case TouchPhase.Ended:
                    // Menandakan bahwa objek selesai dalam proses rotasi
                    isRotating = false;
                    break;
            }
        }
        else
        {
            // Menghentikan rotasi saat tidak ada sentuhan
            isRotating = false;
        }
    }

    void LateUpdate()
    {

    }
}
