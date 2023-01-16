using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour {

    public Button[] levelButtons;
    public int levelForPlayerPrefs = 0;


    private void Start() {
        if (levelForPlayerPrefs != 0) {
            PlayerPrefs.SetInt("levelsUnlocked", levelForPlayerPrefs);
        }

        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = levelsUnlocked; i < levelButtons.Length; i++) {
            levelButtons[i].interactable = false;
        }
    }

    public void SelectLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}
