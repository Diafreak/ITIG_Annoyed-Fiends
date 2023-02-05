using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SettingsUI : MonoBehaviour {

    [Header("Screen Resolution")]
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] screenResolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;


    private void Start() {
        InitializeScreenResolutionDropdown();
    }


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


    public void ToggleFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}