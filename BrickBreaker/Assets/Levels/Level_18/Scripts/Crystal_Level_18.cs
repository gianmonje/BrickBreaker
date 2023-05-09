using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Level_18 : MonoBehaviour {
    public GameObject shield;
    public Brick[] bricks;

    private void OnEnable() {
        shield.SetActive(true);
    }

    public void CrystalCheck() {
        for (int i = 0; i < bricks.Length; i++) {
            if (bricks[i].IsAlive) {
                return;
            }
        }
        shield.SetActive(false);
    }
}
