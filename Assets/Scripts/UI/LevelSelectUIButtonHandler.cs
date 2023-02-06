using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelSelectUIButtonHandler : MonoBehaviour {

    [Header("Image Colors")]
    public Color activatedImageColor;
    public Color deactivatedImageColor;
    [Header("Text Colors")]
    public Color activatedTextColor;
    public Color deactivatedTextColor;

    [Header("Level Select Buttons")]
    public Button[] levelButtons;

    [Header("Reset Player Prefs")]
    public int levelForPlayerPrefs = 0;

    [Header("Main Menu Scene")]
    public string mainMenuSceneName = "MainMenu";


    private void Awake() {
        // just for Resetting the PlayerPrefs
        if (levelForPlayerPrefs != 0) {
            PlayerPrefs.SetInt("levelsUnlocked", levelForPlayerPrefs);
        }

        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);  // 1 = Default Value when "levelsUnlocked" is not yet set

        for (int i = 0; i < levelButtons.Length; i++) {
            if (i < levelsUnlocked) {
                levelButtons[i].interactable = true;
                levelButtons[i].transform.Find("Level"+(i+1)+"Text").GetComponent<TMP_Text>().color = activatedTextColor;
                levelButtons[i].transform.Find("Level"+(i+1)+"Text").GetComponent<TMP_Text>().enableVertexGradient = true;
                levelButtons[i].transform.Find("Level"+(i+1)+"Image").GetComponent<Image>().color = activatedImageColor;
            } else {
                levelButtons[i].interactable = false;
                levelButtons[i].transform.Find("Level"+(i+1)+"Text").GetComponent<TMP_Text>().color = deactivatedTextColor;
                levelButtons[i].transform.Find("Level"+(i+1)+"Text").GetComponent<TMP_Text>().enableVertexGradient = false;
                levelButtons[i].transform.Find("Level"+(i+1)+"Image").GetComponent<Image>().color = deactivatedImageColor;
            }
        }
    }


    public void SelectLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }


    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
