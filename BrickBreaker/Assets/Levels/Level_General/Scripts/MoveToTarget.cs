using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTarget : MonoBehaviour {
    public Transform targetPosition;
    private Vector3 targetPositionVector;
    public float speed;
    public bool freezePositionX;
    public bool freezePositionY;
    public UnityEvent onTargetPos;

    private bool isAnimate;

    public void Play() {
        targetPositionVector = targetPosition.position;
        isAnimate = true;
    }

    public void Play(Transform targetPosition) {
        this.targetPositionVector = targetPosition.position;
        isAnimate = true;
    }

    private void OnEnable() {
        isAnimate = false;
    }

    // Update is called once per frame
    private void Update() {
        if (!isAnimate) return;
        Vector2 target = targetPositionVector;
        if (freezePositionX) target = new Vector2(transform.position.x, targetPositionVector.y);
        if (freezePositionY) target = new Vector2(targetPositionVector.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) <= 0.1f) {
            if (onTargetPos != null) onTargetPos.Invoke();
            isAnimate = false;
        }
    }
}
