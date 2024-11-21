using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewSlideUp : MonoBehaviour
{
    public RectTransform content;
    public float transitionDuration = 0.5f; // Durasi transisi dalam detik
    // Start is called before the first frame update
    void Start()
    {
        if (content == null)
        {
            content = GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
