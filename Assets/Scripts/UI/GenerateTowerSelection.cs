using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateTowerSelection : MonoBehaviour
{

    [SerializeField] private List<TowerTypeSO> towerTypeSOList;

    private static String BUTTON_TOWER     = "Button_Tower";
    private static String TEXT_TOWER_NAME  = "Text_TowerName";
    private static String PANEL_TOWER_NAME = "Panel_TowerName";

    void Start()
    {
        // Get Template from which to create the Selection from
        GameObject towerSelectionTemplate = transform.Find(BUTTON_TOWER).gameObject;
        GameObject towerSelection;

        foreach (TowerTypeSO towerType in towerTypeSOList) {
            // Create Button
            towerSelection = Instantiate(towerSelectionTemplate, transform);
            // Set Button Name
            towerSelection.transform.Find(PANEL_TOWER_NAME).Find(TEXT_TOWER_NAME).GetComponent<TMP_Text>().text = towerType.towerName;
            // Set Button Image
            towerSelection.GetComponent<Button>().image.sprite = towerType.towerIcon;
        }

        // Destroy the Template so it doesn't show up on the UI
        Destroy(towerSelectionTemplate);
    }

}
