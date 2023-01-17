using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    private bool gameOver = false;
    public string nextLevelName = "Level2";
    public int    nextLevel     = 2;

    public GameObject GameOverUI;


    private void Start() {
        if (GameOverUI.activeSelf) {
            GameOverUI.SetActive(false);
        }
    }


    void Update() {

        if (gameOver) {
            return;
        }

        if(PlayerStats.lives <= 0) {
            LostLevel();
        }
    }


    public void WinLevel() {
        Debug.Log("Level Won!");
        PlayerPrefs.SetInt("levelsUnlocked", nextLevel);
        SceneManager.LoadScene(nextLevelName);
    }

    void LostLevel() {
        gameOver = true;
        GameOverUI.SetActive(true);
    }
}
