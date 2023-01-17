using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour {

    public string menuSceneName = "MainMenu";


    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Menu() {
        SceneManager.LoadScene(menuSceneName);
    }

}
