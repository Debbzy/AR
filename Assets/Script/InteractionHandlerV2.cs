using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandlerV2 : MonoBehaviour
{
    public GameObject[] information; // Array of information panels or objects
    public int aValue; // Value that can be shared between colliders

    // List of other InteractionHandlers (to support multiple colliders)
    public List<InteractionHandlerV2> otherColliders;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to handle interaction
    public void HandleInteraction(int value)
    {
        // Update aValue to the passed value
        aValue = value;

        // Disable all information objects
        foreach (GameObject obj in information)
        {
            obj.SetActive(false);
        }

        // Enable the specific information object based on aValue
        if (aValue >= 0 && aValue < information.Length)
        {
            information[aValue].SetActive(true);
        }
    }

    // Method to deactivate all information objects
    public void DeactivateAll()
    {
        foreach (GameObject obj in information)
        {
            obj.SetActive(false);
        }
    }

    // Example of detecting interaction for a collider
    private void OnMouseDown()
    {
        // Assuming this script is attached to a collider,
        // and each object has a specific index to match the information array.
        for (int i = 0; i < information.Length; i++)
        {
            if (this.gameObject.name == "Object" + i) // Example: Object0, Object1, etc.
            {
                HandleInteraction(i); // Call the method with the detected index
                break;
            }
        }
    }

    // Method for reading aValue from a specific collider and activating the corresponding object
    public void ReadAndActivate()
    {
        InteractionHandlerV2 targetCollider = otherColliders.Find(collider => collider != null && collider.aValue == aValue);

        if (targetCollider != null)
        {
            // Disable all information objects
            foreach (GameObject obj in information)
            {
                obj.SetActive(false);
            }

            // Enable the specific information object based on the matched collider's aValue
            if (aValue >= 0 && aValue < information.Length)
            {
                information[aValue].SetActive(true);
            }
        }
    }
}
