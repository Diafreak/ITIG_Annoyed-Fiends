using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuUI : MonoBehaviour {

    public GameObject PauseUI;

    public string menuSceneName = "MainMenu";


    private void Start() {
        if (PauseUI.activeSelf) {
            ToggleVisible();
        }
    }


    public void ToggleVisible() {
        PauseUI.SetActive(!PauseUI.activeSelf);

        if (PauseUI.activeSelf) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }


    public void Continue() {
        ToggleVisible();
    }

    public void Retry() {
        TownerSpawner.enemiesAlive = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Menu() {
        TownerSpawner.enemiesAlive = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
