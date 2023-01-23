using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuUI : MonoBehaviour {

    public GameObject pauseUI;


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

}
