using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour {

    public string menuSceneName = "MainMenu";
    public string currentLevelName;


    public void Retry() {
        SceneManager.LoadScene(currentLevelName);
    }


    public void Menu() {
        SceneManager.LoadScene(menuSceneName);
    }

}
