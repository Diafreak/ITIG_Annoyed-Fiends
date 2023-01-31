using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelSelectUI : MonoBehaviour {

    [Header("Colors")]
    public Color activatedTextColor;
    public Color deactivatedTextColor;

    [Header("Level Select Buttons")]
    public Button[] levelButtons;

    [Header("Reset Player Prefs")]
    public int levelForPlayerPrefs = 0;


    private void Start() {
        // just for Resetting the PlayerPrefs
        if (levelForPlayerPrefs != 0) {
            PlayerPrefs.SetInt("levelsUnlocked", levelForPlayerPrefs);
        }

        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);  // 1 = Default Value when "levelsUnlocked" is not yet set

        for (int i = 0; i < levelButtons.Length; i++) {
            if (i < levelsUnlocked) {
                levelButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().color = activatedTextColor;
            } else {
                levelButtons[i].interactable = false;
                levelButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().color = deactivatedTextColor;
            }
        }
    }


    public void SelectLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}
