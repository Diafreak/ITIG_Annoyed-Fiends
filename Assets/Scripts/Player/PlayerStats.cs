using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private static int money;
    public int startMoney = 10000;

    public static int lives;
    public int startLives = 20;


    private void Start() {
        money = startMoney;
        lives = startLives;
    }


    public static int GetMoney() {
        return money;
    }

    public static void AddMoney(int amount) {
        money += amount;
    }

    public static void SubtractMoney(int amount) {
        money -= amount;
    }
}
