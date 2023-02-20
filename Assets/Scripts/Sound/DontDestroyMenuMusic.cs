using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMenuMusic : MonoBehaviour {

    // Singleton
    static DontDestroyMenuMusic instance;

    private void Start() {
        // Singleton
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }


    private void Update() {
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "LevelSelect") {
            Destroy(gameObject);
        }
    }
}