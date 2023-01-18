using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour {

    public string menuSceneName = "MainMenu";


    public void Retry() {
        TownerSpawner.enemiesAlive = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Menu() {
        TownerSpawner.enemiesAlive = 0;
        SceneManager.LoadScene(menuSceneName);
    }

}
