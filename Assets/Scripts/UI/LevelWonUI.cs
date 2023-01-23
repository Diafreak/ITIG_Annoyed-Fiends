using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelWonUI : MonoBehaviour {

    public string menuSceneName = "MainMenu";
    public string nextLevelName;

    public GameObject levelWonUI;


    private void Start() {
        if (levelWonUI.activeSelf) {
            ToggleVisible();
        }
    }


    public void NextLevel() {
        EnemySpawner.enemiesAlive = 0;
        SceneManager.LoadScene(nextLevelName);
    }

    public void Menu() {
        EnemySpawner.enemiesAlive = 0;
        SceneManager.LoadScene(menuSceneName);
    }


    public void ToggleVisible() {
        levelWonUI.SetActive(!levelWonUI.activeSelf);
    }
}
