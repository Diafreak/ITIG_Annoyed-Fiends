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


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Menu() {
        SceneManager.LoadScene(menuSceneName);
    }
}
