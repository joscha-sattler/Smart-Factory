using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    // Attribute 

    public AudioMixer audioMixer;

    public Dropdown aufloesungDropdown;

    Resolution[] resolutions;

    // Methoden

    void Start()
    {
        resolutions = Screen.resolutions;

        aufloesungDropdown.ClearOptions();

        int aktuelleAufloesung = 0;

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                aktuelleAufloesung = i;

            }

        }

        aufloesungDropdown.AddOptions(options);

        aufloesungDropdown.value = aktuelleAufloesung;

        aufloesungDropdown.RefreshShownValue();

    }

    //Audio
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Grafik
    public void setGraphik(int qualityIndex)
    {

        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Fullscreen
    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //update Screengröße
    public void SetAufloesung(int aufloesungsIndex)
	{
        Resolution resolution = resolutions[aufloesungsIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

}