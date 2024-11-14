using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMenu : MonoBehaviour
{
    public RectTransform content;
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
    public void SwitchScroll(string aValue)
    {
        if (aValue == "Scan")
        {
            Vector3 ScanLocal = new Vector3(0, 0, 0);
            Vector2 scanOffset = new Vector2(0, 0);
            content.localPosition = ScanLocal;
            content.offsetMax = scanOffset;
        }
        else if (aValue == "Zoom")
        {
            Vector3 ZoomLocal = new Vector3(-472, 0, 0);
            Vector2 ZoomOffset = new Vector2(-472, 0);
            content.localPosition = ZoomLocal;
            content.offsetMax = ZoomOffset;
        }
    }
}
