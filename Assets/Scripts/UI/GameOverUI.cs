using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour {

    public string menuSceneName = "MainMenu";

    public GameObject gameOverUI;


    private void Start() {
        if (gameOverUI.activeSelf) {
            ToggleVisible();
        }
    }


    public void Retry() {
        EnemySpawner.enemiesAlive = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu() {
        EnemySpawner.enemiesAlive = 0;
        SceneManager.LoadScene(menuSceneName);
    }


    public void ToggleVisible() {
        gameOverUI.SetActive(!gameOverUI.activeSelf);
    }
}
