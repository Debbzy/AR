using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Vuforia;
using Unity.VisualScripting;

public class ImageTargetManager : MonoBehaviour
{
    [System.Serializable]
    public class BookImageTarget
    {
        public string bookRegion;
        public ImageTargetBehaviour bookImageTargetBehaviour;
        public GestureObserver gestureObserver;
        public GameObject joyStick;
        public GameObject avatarStick;

    }
    [System.Serializable]
    public class CardImageTarget
    {
        public string cardRegion;
        public ImageTargetBehaviour cardImageTargetBehaviour;
        public AudioSource audioSource;
    }


    public BookImageTarget[] bookImageTargets;
    public CardImageTarget[] cardImageTargets;
    public AudioClip[] audioClip;
    public UnityEvent defaultEvents;
    public UnityEvent warningEvents;
    public InteractionHandler interactionHandler;
    public CardManager cardManager;
    public Button buttonScan;
    public Button buttonCard;

    public bool cardActive = false;
    public bool bookActive = false;
    private bool switchSong;
    public string regionNow;
    public string cameraInteractionContent;
    private string cameraInteractionContentHidden = "Scan";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bookActive)
        {
            foreach (var bookImageTarget in bookImageTargets)
            {
                if (bookImageTarget.bookImageTargetBehaviour.enabled)
                {
                    HandleCameraInteraction(bookImageTarget);

                    if (cameraInteractionContent == "Scan")
                    {
                        HandleRegionScan(regionNow);
                        buttonScan.interactable = true;
                    }
                }
            }
        }
        else if (cardActive)
        {
            HandleCardInteraction();
            foreach (var cardImageTarget in cardImageTargets)
            {
                if (cardImageTarget.cardRegion == regionNow)
                {
                    UpdateAudioState(cardImageTarget);  
                }
            }
        }
    }

    private void HandleCameraInteraction(BookImageTarget bookImageTarget)
    {
        switch (cameraInteractionContent)
        {
            case "Zoom":
                bookImageTarget.gestureObserver.enabled = true;
                bookImageTarget.joyStick.SetActive(false);
                bookImageTarget.avatarStick.SetActive(false);
                break;

            case "Avatar":
                bookImageTarget.gestureObserver.enabled = false;
                bookImageTarget.joyStick.SetActive(true);
                bookImageTarget.avatarStick.SetActive(true);
                break;

            default:
                bookImageTarget.gestureObserver.enabled = false;
                bookImageTarget.joyStick.SetActive(false);
                bookImageTarget.avatarStick.SetActive(false);
                break;
        }
    }

    private void HandleRegionScan(string region)
    {
        var regionMapping = new Dictionary<string, int>
        {
            { "IndoBook", 0 },
            { "FranceBook", 1 },
            { "AmerikaBook", 2 },
            { "BrazilBook", 3 },
            { "MesirBook", 4 }
        };

        if (regionMapping.TryGetValue(region, out int interactionIndex))
        {
            interactionHandler.HandleInteraction(interactionIndex);
        }
    }

    private void HandleCardInteraction()
    {
        if (cameraInteractionContent == "Card")
        {
            buttonCard.interactable = true;
        }
        else
        {
            buttonCard.interactable = false;
        }
    }
    public void TargetFound(ImageTargetBehaviour imageTargetBehaviours)
    {
        foreach (var bookImageTarget in bookImageTargets)
        {
            if (imageTargetBehaviours.TargetName == bookImageTarget.bookRegion)
            {
                bookActive = true;
                cardActive = false;
                regionNow = bookImageTarget.bookRegion;
                EnableBookTarget(bookImageTarget);
                HandleCameraInteraction(bookImageTarget);
                CameraContent(cameraInteractionContentHidden);
            }
            else
            {
                DisableBookTarget(bookImageTarget);
            }
        }

        foreach (var cardImageTarget in cardImageTargets)
        {
            if (imageTargetBehaviours.TargetName == cardImageTarget.cardRegion && cameraInteractionContent == "Card")
            {
                cardActive = true;
                bookActive = false;
                regionNow = cardImageTarget.cardRegion;
                cardImageTarget.cardImageTargetBehaviour.enabled = true;
                CardFound(imageTargetBehaviours);
                CameraContent(cameraInteractionContentHidden);
                defaultEvents?.Invoke();
            }
            else if (imageTargetBehaviours.TargetName == cardImageTarget.cardRegion && cameraInteractionContent != "Card")
            {
                cardActive = true;
                bookActive = false;
                regionNow = cardImageTarget.cardRegion;
                CameraContent(cameraInteractionContentHidden);
                warningEvents?.Invoke();
            }

        }
    }
    public void TargetLost()
    {
        bookActive = false;
        cardActive = false;
        buttonScan.interactable = false;
        buttonCard.interactable = false;
        regionNow = "";
        cameraInteractionContent = "";

        foreach (var bookImageTarget in bookImageTargets)
        {
            EnableBookTarget(bookImageTarget);
        }
        foreach (var cardImageTarget in cardImageTargets)
        {
            cardImageTarget.cardImageTargetBehaviour.enabled = true;
            cardImageTarget.audioSource.Pause();
        }
    }

    private void EnableBookTarget(BookImageTarget bookImageTarget)
    {
        bookImageTarget.bookImageTargetBehaviour.enabled = true;
    }

    private void DisableBookTarget(BookImageTarget bookImageTarget)
    {
        bookImageTarget.bookImageTargetBehaviour.enabled = false;
        bookImageTarget.gestureObserver.enabled = false;
        bookImageTarget.joyStick.SetActive(false);
        bookImageTarget.avatarStick.SetActive(false);
    }
    private void CardFound(ImageTargetBehaviour imageTargetBehaviours)
    {
        foreach (var bookImageTarget in bookImageTargets)
        {
            // Matikan komponen lainnya
            bookImageTarget.bookImageTargetBehaviour.enabled = false;
        }  
    }
    private void UpdateAudioState(CardImageTarget cardImageTarget)
    {
        if (!switchSong)
        {
            if (cardImageTarget.audioSource.isPlaying)
            {
                cardImageTarget.audioSource.Pause();
            }
        }
        else
        {
            if (!cardImageTarget.audioSource.isPlaying)
            {
                cardImageTarget.audioSource.Play();
            }
        }
    }
    public void SwitchFunction()
    {
        switchSong = !switchSong; // Membalik nilai switchSong
    }
    public void CameraContent(string aValue)
    {
        cameraInteractionContent = aValue;
        cameraInteractionContentHidden = aValue;
    }
}
