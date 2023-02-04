using UnityEngine;


[CreateAssetMenu(fileName = "GridTileSO", menuName = "ScriptableObjects/GridTileSO")]
public class GridTileSO : ScriptableObject {

    [Header("Prefab")]
    public Transform visual;

    [Header("Colors")]
    public Color defaultColor;
    public Color highlightColor;
    public Color insufficientMoneyColor;
}
