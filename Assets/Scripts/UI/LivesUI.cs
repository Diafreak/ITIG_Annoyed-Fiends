using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LivesUI : MonoBehaviour {
    public TMP_Text livesText;

    void Update() {
        livesText.text = "Lives: " + PlayerStats.lives.ToString();
    }
}
