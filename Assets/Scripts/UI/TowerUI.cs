using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public GameObject ui;

    public void SetTarget(Vector3 position) {
        // if the same tile is clicked again, hide the Upgrade/Sell-Menu
        if (transform.position == position && ui.activeSelf) {
            ui.SetActive(false);

        // show & move the Upgrade/Sell-Menu over the selected Tower
        } else {
            transform.position = position;
            ui.SetActive(true);
        }
    }

    public void Hide() {
        ui.SetActive(false);
    }
}
