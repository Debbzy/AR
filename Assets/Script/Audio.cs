using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetMasterVolume(float volume)
    {
        if (volume == -40)
        {
            audioMixer.SetFloat("MainAudio", -80);
        }
        else
        {
            audioMixer.SetFloat("MainAudio", volume);
        }
    }
}
