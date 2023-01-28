using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuUI : MonoBehaviour {

    public GameObject pauseUI;

    private float previousGameSpeed;


    public void ToggleVisible() {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf) {
            previousGameSpeed = Time.timeScale;
            Time.timeScale = 0f;
        } else {
            Time.timeScale = previousGameSpeed;
        }
    }


    public void Continue() {
        ToggleVisible();
    }

}
