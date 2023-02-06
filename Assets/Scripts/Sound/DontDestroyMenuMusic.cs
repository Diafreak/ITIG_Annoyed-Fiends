using UnityEngine;


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
}