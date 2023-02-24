using UnityEngine;


public class AudioManager : MonoBehaviour {

    private void Start() {

        // destroy Menu-Music when loading a level
        if (DontDestroyMenuMusic.instance != null) {
            Destroy(DontDestroyMenuMusic.instance.gameObject);
        }
    }
}
