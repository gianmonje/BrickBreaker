using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {
    private Animator animator;
    private void OnEnable() {
        animator = GetComponent<Animator>();
        animator.SetTrigger("reset");
    }
    public void DoHitEffetct() {
        animator.SetTrigger("hit");
    }
}
