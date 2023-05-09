using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSummoner : MonoBehaviour {
    public Vector2 minMaxTime;
    public Vector2 minMaxSize;
    public Vector2 minMaxXOffset;
    public int summonTimes = 0;
    public GameObject bubblePrefab;
    public Transform targetPosition;
    public bool dontScale = false;
    public bool showOnAwake = true;

    protected event Action<GameObject> onSummoningComplete;
    public event Action<GameObject> OnSummoningComplete {
        add { onSummoningComplete += value; }
        remove { onSummoningComplete += value; }
    }

    protected int summonCounter;
    // Start is called before the first frame update
    protected void OnEnable() {
        if (!showOnAwake) return;
        summonCounter = summonTimes;

        StartSummoning();
    }


    public void StartSummoning() {
        StartCoroutine("ShowBubble");
    }

    public void Stop() {
        StopCoroutine("ShowBubble");
    }

    protected virtual IEnumerator ShowBubble() {
        Debug.Log("SUMMON CALLED!");
        float delay = UnityEngine.Random.Range(minMaxTime.x, minMaxTime.y + 1);
        if (minMaxTime.x > 0 && minMaxTime.y > 0) yield return new WaitForSeconds(delay);
        Debug.Log("SUMMONED!");
        GameObject go = Instantiate(bubblePrefab, this.transform.parent, true);
        float diceSize = UnityEngine.Random.Range(minMaxSize.x, minMaxSize.y);
        if (!dontScale) go.transform.localScale = new Vector2(diceSize, diceSize);
        float randomXOffset = UnityEngine.Random.Range(minMaxXOffset.x, minMaxXOffset.y);
        Vector2 spawnPos = new Vector2(targetPosition.position.x + randomXOffset, targetPosition.position.y);
        go.transform.localPosition = spawnPos;
        if (onSummoningComplete != null) onSummoningComplete(go);

        if (summonTimes <= 0) {
            StartCoroutine("ShowBubble");
        } else {
            if (summonCounter > 0) {
                StartCoroutine("ShowBubble");
                summonCounter--;
            }
        }
    }
}
