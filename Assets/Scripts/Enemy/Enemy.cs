using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

    [Header("Attributes")]
    public float startHp = 100;
    public int killValue = 100;
    public float speed = 10f;

    [Header("Health Bar Visual")]
    public Image healthBar;

    private float hp;


    private void Start() {
        hp = startHp;
    }


    public void TakeDamage(float amount) {
        hp -= amount;
        healthBar.fillAmount = hp / startHp;

        if (hp <= 0) {
            Die();
        }
    }

    private void Die() {
        PlayerStats.AddMoney(killValue);
        EnemySpawner.enemiesAlive--;
        Destroy(gameObject);
    }

}
