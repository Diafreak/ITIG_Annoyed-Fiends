using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GridTileSO", menuName = "ScriptableObjects/GridTileSO")]
public class GridTileSO : ScriptableObject {

    public Transform visual;
    public Color defaultColor;
    public Color highlightColor;
    public Color insufficientMoneyColor;

}
