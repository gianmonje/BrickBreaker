using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSun : MonoBehaviour {
    private Brick brick { get { return GetComponent<Brick>(); } }
    private int Hitpoints { get { return brick.brickData.hitpoints; } }
    private SpriteRenderer SunSprite { get { return brick.currentHealthSprite.spriteGameObject.GetComponent<SpriteRenderer>(); } }
    private float transparencyToReduce;
    private byte currentTransparency;

    private void OnEnable() {
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart() {
        yield return new WaitForSeconds(0.1f);
        SunSprite.color = new Color32(255, 255, 255, 255);
        transparencyToReduce = 255f / (float)Hitpoints;
        currentTransparency = 255;
    }

    public void ReduceTransparency() {
        currentTransparency -= (byte)transparencyToReduce;
        SunSprite.color = new Color32(255, 255, 255, currentTransparency);
    }
}
