using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickDeathEffect : MonoBehaviour {
    public UnityEvent OnBrickDeathEffectDone;
    public string animationToPlayOnEnable;

    public void OnEnable() {
        animationToPlayOnEnable = "start";
        if (animationToPlayOnEnable.Length > 0 && GetComponent<Animator>() != null) GetComponent<Animator>().SetTrigger(animationToPlayOnEnable);
    }

    public void TriggerEventBrickDeathAnimationDone() {
        if (OnBrickDeathEffectDone != null) OnBrickDeathEffectDone.Invoke();
    }
}
