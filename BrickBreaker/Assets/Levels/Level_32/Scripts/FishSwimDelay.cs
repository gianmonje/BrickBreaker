using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwimDelay : MonoBehaviour {
    public Vector2 minMaxTime;
    // Start is called before the first frame update
    void OnEnable() {
        StopCoroutine("ShowBubble");
        StartCoroutine("ShowBubble");
    }
    private IEnumerator ShowBubble() {
        float delay = Random.Range(minMaxTime.x, minMaxTime.y + 1);
        yield return new WaitForSeconds(delay);
        GetComponent<Animator>().SetTrigger("stop");
    }
}
