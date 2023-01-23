using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuAndRetryButtons : MonoBehaviour {

    public string menuSceneName = "MainMenu";


    public void Menu() {
        SceneManager.LoadScene(menuSceneName);
        Time.timeScale = 1f;
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
