using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupStarForceManager : MonoBehaviour {
    public GameObject[] starForces;

    public void ShowAStar() {
        GameObject star = GetAvailableStar();
        if (star != null) star.SetActive(true);
    }

    private GameObject GetAvailableStar() {
        for (int i = 0; i < starForces.Length; i++) {
            if (!starForces[i].activeInHierarchy) return starForces[i];
        }
        return null;
    }

    private void OnDisable() {
        for (int i = 0; i < starForces.Length; i++) {
            starForces[i].SetActive(false);
        }
    }
}
