using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelWonUI : MonoBehaviour {

    public string nextLevelName;


    public void NextLevel() {
        SceneManager.LoadScene(nextLevelName);
        EnemySpawner.enemiesAlive = 0;
    }
}
