using UnityEngine;


[CreateAssetMenu(fileName = "GridTileSO", menuName = "ScriptableObjects/GridTileSO")]
public class GridTileSO : ScriptableObject {

    [Header("Prefab")]
    public Transform visual;

    [Header("Default Colors")]
    public Color defaultColor;
    public Color defaultEmissionColor;

    [Header("Highlight Colors")]
    public Color highlightColor;
    public Color highlightEmissionColor;

    [Header("Insufficient Money Colors")]
    public Color insufficientMoneyColor;
    public Color insufficientMoneyEmissionColor;
}
