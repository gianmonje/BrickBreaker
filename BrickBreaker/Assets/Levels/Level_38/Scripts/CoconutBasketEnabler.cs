using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBasketEnabler : MonoBehaviour {

    public BoxCollider2D[] boxCollider2Ds;
    public float delay;

    public void OnEnable() {
        StartCoroutine(EnableWithDelay(true));
    }

    public void EnableCollidersWithDelay(bool isEnabled) {
        StartCoroutine(EnableWithDelay(isEnabled));
    }

    private IEnumerator EnableWithDelay(bool isEnabled) {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < boxCollider2Ds.Length; i++) {
            boxCollider2Ds[i].enabled = isEnabled;
        }
    }
}
