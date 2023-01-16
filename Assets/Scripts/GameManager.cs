using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public string nextLevelName = "Level2";
    public int    nextLevel     = 2;


    // Game Over ...


    public void WinLevel() {
        Debug.Log("Level Won!");
        PlayerPrefs.SetInt("levelsUnlocked", nextLevel);
        SceneManager.LoadScene(nextLevelName);
    }
}
