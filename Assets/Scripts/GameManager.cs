using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    private bool gameOver = false;
    public string nextLevelName = "Level2";
    public int    nextLevel     = 2;

    public GameObject gameOverUI;
    public PauseMenuUI pauseMenuUI;


    private void Start() {
        if (gameOverUI.activeSelf) {
            gameOverUI.SetActive(false);
        }
    }


    void Update() {

        if (gameOver) {
            return;
        }

        if (PlayerStats.lives <= 0) {
            LostLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenuUI.ToggleVisible();
        }
    }


    public void WinLevel() {
        Debug.Log("Level Won!");
        PlayerPrefs.SetInt("levelsUnlocked", nextLevel);
        SceneManager.LoadScene(nextLevelName);
    }

    void LostLevel() {
        gameOver = true;
        gameOverUI.SetActive(true);
    }
}
