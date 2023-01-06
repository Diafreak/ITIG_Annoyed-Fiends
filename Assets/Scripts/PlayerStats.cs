using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static int money;
    public int startMoney = 10000;

    private void Start() {
        money = startMoney;
    }
}
