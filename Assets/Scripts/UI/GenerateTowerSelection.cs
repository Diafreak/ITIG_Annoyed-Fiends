using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateTowerSelection : MonoBehaviour
{
    private static String BUTTON_TOWER     = "Button_Tower";
    private static String TEXT_TOWER_NAME  = "Text_TowerName";
    private static String PANEL_TOWER_NAME = "Panel_TowerName";

    void Start()
    {
        // Get Template from which to create the Selection from
        GameObject towerSelectionTemplate = transform.Find(BUTTON_TOWER).gameObject;
        GameObject towerSelection;

        for (int i = 0; i < 3; i++) {   // change 3 to getAllTowerSize() or whatever
            towerSelection = Instantiate(towerSelectionTemplate, transform);
            // Set Tower-Name
            towerSelection.transform.Find(PANEL_TOWER_NAME).Find(TEXT_TOWER_NAME).GetComponent<TMP_Text>().text = "Test " + i; // give list of all towers
        }

        // Destroy the Template so it doesn't show up on the UI
        Destroy(towerSelectionTemplate);
    }

}
