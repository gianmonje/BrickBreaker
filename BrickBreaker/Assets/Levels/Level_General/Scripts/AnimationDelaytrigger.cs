using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationDelaytrigger : MonoBehaviour {
    public float delay;
    public string triggerID;
    public string stopTriggerID;
    public UnityEvent onDone;

    // Start is called before the first frame update
    private void OnEnable() {
        StopCoroutine("DelayTrigger");
        StartCoroutine("DelayTrigger");
    }

    private IEnumerator DelayTrigger() {
        GetComponent<Animator>().SetTrigger(stopTriggerID);
        yield return new WaitForSeconds(delay);
        GetComponent<Animator>().SetTrigger(triggerID);
    }

    public void SetDoneAnimation() {
        GetComponent<Animator>().SetTrigger(stopTriggerID);
        if (onDone != null) onDone.Invoke();
    }
}
