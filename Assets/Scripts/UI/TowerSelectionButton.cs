using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionButton : MonoBehaviour
{
    private static String TEXT_TOWER_NAME  = "Text_TowerName";
    private static String PANEL_TOWER_NAME = "Panel_TowerName";

    private Button button;
    [SerializeField] private GridBuildingSystem grid;


    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetSelectedTower);
    }

    private void SetSelectedTower() {
        grid.SetSelectedTower(button.transform.Find(PANEL_TOWER_NAME).Find(TEXT_TOWER_NAME).GetComponent<TMP_Text>().text);
    }
}
