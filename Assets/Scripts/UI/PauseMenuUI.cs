using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuUI : MonoBehaviour {

    public GameObject pauseUI;

    public string menuSceneName = "MainMenu";


    private void Start() {
        if (pauseUI.activeSelf) {
            ToggleVisible();
        }
    }


    public void ToggleVisible() {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }


    public void Continue() {
        ToggleVisible();
    }

    public void Retry() {
        EnemySpawner.enemiesAlive = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Menu() {
        EnemySpawner.enemiesAlive = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
