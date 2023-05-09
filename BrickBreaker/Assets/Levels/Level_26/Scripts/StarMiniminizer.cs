using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMiniminizer : MonoBehaviour {
    private Vector2 initSize;
    private void OnEnable() {
        initSize = transform.localScale;
        ResetSize();
    }

    public void ResetSize() {
        transform.localScale = initSize;
    }

    public void ReduceSize(float reduceAmount) {
        transform.localScale = new Vector2(Mathf.Clamp(transform.localScale.x - reduceAmount, 0.1f, 2), Mathf.Clamp(transform.localScale.y - reduceAmount, 0.1f, 2));
    }
}
