using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Rect SliderLocation;

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("Volume", vol);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


    public void ChangeBrightness(float brightness)
    {

        RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1.0f);

    }
}
