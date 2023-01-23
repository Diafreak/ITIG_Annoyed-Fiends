using UnityEngine;


[CreateAssetMenu(fileName = "GridTileSO", menuName = "ScriptableObjects/GridTileSO")]
public class GridTileSO : ScriptableObject {

    public Transform visual;
    public Color defaultColor;
    public Color highlightColor;
    public Color insufficientMoneyColor;
}
