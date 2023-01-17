using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveUI : MonoBehaviour
{
    public TMP_Text liveText;

    // Update is called once per frame
    void Update()
    {
        liveText.text = "Leben: " + PlayerStats.lives.ToString();
    }
}
