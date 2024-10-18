using UnityEngine;
using UnityEngine.UI;


public class ObjectInteraction : MonoBehaviour
{
    // Referensi ke objek 3D
    public GameObject targetObject;

    // Kecepatan rotasi
    public float rotationSpeed = 50f;

    // Method untuk merotasi objek
    public void RotateObjectRight()
    {
        if (targetObject != null)
        {
            targetObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    public void RotateObjectLeft()
    {
        if (targetObject != null)
        {
            targetObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
}
