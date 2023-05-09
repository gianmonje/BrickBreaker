using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level16LayerManager : MonoBehaviour {
    public Brick[] bricks;

    public void ShowKeys() {
        for (int i = 0; i < bricks.Length; i++) {
            bricks[i].gameObject.SetActive(true);
        }
    }

    public void HideKeys() {
        for (int i = 0; i < bricks.Length; i++) {
            bricks[i].brickDeathEffect.gameObject.GetComponent<Animator>().SetTrigger("fade");
        }
    }

    public void HideKeysFast() {
        for (int i = 0; i < bricks.Length; i++) {
            bricks[i].gameObject.SetActive(false);
        }
    }

}
