using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class SettingsUI : MonoBehaviour {

    [Header("Screen Resolution")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    [Header("Audio Mixer")]
    public AudioMixer masterMixer;

    [Header("Sliders")]
    public Slider brightnessSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;
    public Slider smoothingSlider;

    [Header("Volume for Exposure")]
    public Volume postProcessingVolume;

    [Header("Brightness-Slider Snap Threshold")]
    public float brightnessSliderThreshold = 0.1f;

    private Resolution[] screenResolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;


    private void Start() {
        InitializeScreenResolutionDropdown();
        InitializeFullscreenToggle();
        InitializeAudioSliders();
        InitializeBrightnessSlider();
        InitializeMouseSlider();
    }


    // ------------------------------
    // Screen Resolution
    // ------------------------------

    private void InitializeScreenResolutionDropdown() {
        screenResolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        resolutionDropdown.ClearOptions();

        // get all Screen-Resolutions that fit the current Monitors refresh-rate
        for (int i = 0; i < screenResolutions.Length; i++) {
            if (screenResolutions[i].refreshRate == currentRefreshRate) {
                filteredResolutions.Add(screenResolutions[i]);
            }
        }

        // format all Screen-Resolutions and put them into a list
        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++) {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);

            // get the current Screen-Resolution for setting up the Pre-Selection of the Dropdown
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void SetScreenResolution(int index) {
        Resolution selectedResolution = filteredResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }


    public void InitializeFullscreenToggle() {
        fullscreenToggle.isOn = Screen.fullScreen;
    }


    public void ToggleFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }



    // ------------------------------
    // Audio
    // ------------------------------

    private void InitializeAudioSliders() {
        // if PlayerPrefs aren't set yet, the values from the masterMixer are used instead
        masterMixer.GetFloat("MusicVolume", out float musicVolume);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        musicSlider.value = musicVolume;
        masterMixer.SetFloat("MusicVolume", musicVolume);

        masterMixer.GetFloat("SFXVolume", out float sfxVolume);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", sfxVolume);
        sfxSlider.value = sfxVolume;
        masterMixer.SetFloat("SFXVolume", sfxVolume);
    }


    public void AdjustMusicVolume(float volume) {
        masterMixer.SetFloat("MusicVolume", volume);
        // Mute
        if (volume <= musicSlider.minValue) {
            masterMixer.SetFloat("MusicVolume", -80f);
        }
        // Save Volume
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }


    public void AdjustSFXVolume(float volume) {
        masterMixer.SetFloat("SFXVolume", volume);
        // Mute
        if (volume <= musicSlider.minValue) {
            masterMixer.SetFloat("SFXVolume", -80f);
        }
        // Save Volume
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }



    // ------------------------------
    // Brightness
    // ------------------------------

    private void InitializeBrightnessSlider() {
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 0);
    }


    public void AdjustBrightness(float brightnessValue) {

        // snap Slider to 0 when inside the threshold
        if (brightnessValue >= -brightnessSliderThreshold && brightnessValue <= brightnessSliderThreshold) {
            brightnessValue = 0f;
            brightnessSlider.value = brightnessValue;
        }

        // get PostExposure from PostProcessing-Volume
        postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments);
        // activate PostExposure
        colorAdjustments.postExposure.overrideState = true;
        // set PostExposure to Slider-Value
        colorAdjustments.postExposure.value = brightnessValue;
        // Save Brightness
        PlayerPrefs.SetFloat("Brightness", brightnessValue);
    }



    // ------------------------------
    // Mouse Sensitivity
    // ------------------------------

    private void InitializeMouseSlider() {
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 25);
        smoothingSlider.value   = PlayerPrefs.GetFloat("MouseSmoothing", 10);
    }


    public void AdjustSensitivity(float sensitivity) {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }


    public void AdjustMouseSmoothing(float smoothing) {
        PlayerPrefs.SetFloat("MouseSmoothing", smoothing);
    }
}