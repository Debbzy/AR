using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHolding = false;

    // Durasi berapa lama tombol di-hold
    public float holdTime = 0f;
    public UnityEvent OnHold;

    // Event yang dipanggil selama tombol di-hold
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        Debug.Log("Button pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        holdTime = 0f;
        Debug.Log("Button released");
    }

    void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;
            // Debug.Log("Holding button for: " + holdTime + " seconds");
            // Debug.Log("Hold duration reached!");
            OnHold?.Invoke();

        }
    }
}
