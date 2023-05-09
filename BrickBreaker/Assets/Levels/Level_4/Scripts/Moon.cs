using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
    private Animator animator { get { return GameplayHUD.Instance.moonHelpAnimator; } }
    private void OnEnable() {
        animator.gameObject.SetActive(true);
        StartCoroutine(ShowWithDelay(1));
    }

    private void OnDisable() {
        animator.gameObject.SetActive(false);
        animator.SetTrigger("reset");
    }

    private IEnumerator ShowWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("show");
        StartCoroutine(HideWithDelay(3));
    }

    private IEnumerator HideWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("hide");
        StartCoroutine(ShowWithDelay(30));
    }
}
