using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBasket : MonoBehaviour {
    public int index;

    private int initIndex;
    private void Awake() {
        initIndex = index;
    }
    private void OnDisable() {
        index = initIndex;
    }
}
