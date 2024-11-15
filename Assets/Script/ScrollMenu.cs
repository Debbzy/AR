using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMenu : MonoBehaviour
{
    public RectTransform content;
    public float transitionDuration = 0.1f; // Durasi transisi dalam detik
    public Text[] texts;
    public int targetFontSize = 95; // Ukuran font yang ditargetkan
    public int defaultFontSize = 70; // Ukuran font default

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
    public void InformationSlide(float targetPosY)
    {
        StartCoroutine(SmoothTransitionY(targetPosY));

    }
    public void SwitchScroll(string aValue)
    {
        Vector3 targetPosition;
        Vector2 targetOffset;
        int selectedTextIndex = -1;
        
        if (aValue == "Scan")
        {
            targetPosition = new Vector3(0, 0, 0);
            targetOffset = new Vector2(0, 0);
            selectedTextIndex = 0;
        }
        else if (aValue == "Zoom")
        {
            targetPosition = new Vector3(-472, 0, 0);
            targetOffset = new Vector2(-472, 0);
            selectedTextIndex = 3;
        }
        else if (aValue == "Avatar")
        {
            targetPosition = new Vector3(-940, 0, 0);
            targetOffset = new Vector2(-940, 0);
            selectedTextIndex = 4;
        }
        else if (aValue == "Foto")
        {
            targetPosition = new Vector3(472, 0, 0);
            targetOffset = new Vector2(472, 0);
            selectedTextIndex = 1;
        }
        else if (aValue == "Card")
        {
            targetPosition = new Vector3(927, 0, 0);
            targetOffset = new Vector2(927, 0);
            selectedTextIndex = 2;
        }
        else
        {
            return; // Jika nilai aValue tidak valid
        }
        StartCoroutine(SmoothTransition(targetPosition, targetOffset, selectedTextIndex));
    }
    private IEnumerator SmoothTransition(Vector3 targetPosition, Vector2 targetOffset, int selectedTextIndex)
    {
        Vector3 startPosition = content.localPosition;
        Vector2 startOffset = content.offsetMax;
        float elapsedTime = 0f;
        int[] startFontSizes = new int[texts.Length];

        Color selectedColor;
        Color defaultColor;
        ColorUtility.TryParseHtmlString("#FFAA00", out selectedColor); // Warna untuk tombol yang dipilih
        ColorUtility.TryParseHtmlString("#B9B9B9", out defaultColor); // Warna untuk tombol lainnya
        for (int i = 0; i < texts.Length; i++)
        {
            // Simpan ukuran font awal dari setiap teks
            startFontSizes[i] = texts[i].fontSize;
        }

        while (elapsedTime < transitionDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsedTime / transitionDuration);
            // Menginterpolasi posisi dan offset dengan Mathf.Lerp
            content.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            content.offsetMax = Vector2.Lerp(startOffset, targetOffset, t);

            for (int i = 0; i < texts.Length; i++)
            {
                int targetSize = (i == selectedTextIndex) ? targetFontSize : defaultFontSize;
                texts[i].fontSize = Mathf.RoundToInt(Mathf.Lerp(startFontSizes[i], targetSize, t));
                Color targetColor = (i == selectedTextIndex) ? selectedColor : defaultColor;
                texts[i].color = targetColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Mengatur posisi dan offset akhir untuk memastikan akurasi
        content.localPosition = targetPosition;
        content.offsetMax = targetOffset;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].fontSize = (i == selectedTextIndex) ? targetFontSize : defaultFontSize;
            texts[i].color = (i == selectedTextIndex) ? selectedColor : defaultColor;
        }
    }

    private IEnumerator SmoothTransitionY(float targetPosY)
    {
        float startY = content.anchoredPosition.y;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Hitung progress dari 0 hingga 1 dengan smoothing
            float t = Mathf.SmoothStep(0, 1, elapsedTime / transitionDuration);

            // Lakukan transisi pada PosY dengan smoothing
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, Mathf.Lerp(startY, targetPosY, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Mengatur PosY akhir untuk memastikan akurasi
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, targetPosY);
    }
}
