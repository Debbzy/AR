using UnityEngine;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
    void Start()
    {
        // Periksa dan minta izin WRITE_EXTERNAL_STORAGE
        // if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        // {
        //     Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        // }
    }
}