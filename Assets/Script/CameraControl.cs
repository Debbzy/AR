using UnityEngine;
using Vuforia;

public class CameraControl : MonoBehaviour
{
    private VuforiaBehaviour vuforiaBehaviour;

    void Start()
    {
        // Dapatkan referensi ke VuforiaBehaviour
        vuforiaBehaviour = VuforiaBehaviour.Instance;

        // Pastikan Vuforia sudah aktif
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = true; // Aktifkan Vuforia saat aplikasi dimulai
        }
    }

    // Fungsi untuk menghentikan tampilan kamera Vuforia
    public void StopCamera()
    {
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = false; // Matikan VuforiaBehaviour (kamera tidak akan terlihat)
        }

        // Menonaktifkan kamera utama Unity (jika ingin menyembunyikan tampilan kamera Unity)
        Camera.main.enabled = false; // Menonaktifkan kamera utama Unity
    }

    // Fungsi untuk melanjutkan tampilan kamera Vuforia
    public void ResumeCamera()
    {
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = true; // Aktifkan kembali Vuforia
        }

        // Mengaktifkan kembali kamera utama Unity
        Camera.main.enabled = true; // Mengaktifkan kamera utama Unity kembali
    }
}