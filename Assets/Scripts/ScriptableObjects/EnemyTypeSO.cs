using UnityEngine;


[CreateAssetMenu(fileName = "EnemyTypeSO", menuName = "ScriptableObjects/EnemyTypeSO")]
public class EnemyTypeSO : ScriptableObject {

    [Header("Enemy Name")]
    public string enemyName;

    [Header("Enemy Visuals")]
    public Transform prefab;
    public Transform visual;

    [Header("Enemy Stats")]
    public float startHp;
    public int killValue;
    public float speed;
}
