using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject[] information;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HandleInteraction(int aValue)
    {
        foreach (GameObject obj in information)
        {
            obj.SetActive(false);
        }
        if (aValue == 0)
        {
            information[aValue].SetActive(true);
        }
        else if (aValue == 1)
        {
            information[aValue].SetActive(true);
        }
        else if (aValue == 2)
        {
            information[aValue].SetActive(true);
        }
        else if (aValue == 3)
        {
            information[aValue].SetActive(true);
        }
        else if (aValue == 4)
        {
            information[aValue].SetActive(true);
        }
    }
}
