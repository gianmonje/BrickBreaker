using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSummoner : BubbleSummoner {
    public Transform target;

    public void Start() {
        OnSummoningComplete += HeartSummoner_OnSummoningComplete;
    }

    public void OnDestroy() {
        OnSummoningComplete -= HeartSummoner_OnSummoningComplete;
    }

    private void HeartSummoner_OnSummoningComplete(GameObject obj) {
        obj.GetComponent<MoveToTarget>().Play(target);
    }
}
