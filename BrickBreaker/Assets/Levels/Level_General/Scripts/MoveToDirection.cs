using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToDirection : MonoBehaviour {
    public Vector2 targetPosition;
    public float speed;
    public bool freezePositionX;
    public bool freezePositionY;
    public bool incrementStyle = false;

    public UnityEvent onTargetPos;

    private bool isAnimate;
    private bool isAlreadyIncrementing = false;
    private Vector2 targetPostionIncrement;
    private Vector2 initialPos;

    private void Awake() {
        initialPos = transform.position;
    }

    public void Play() {
        if (incrementStyle && !isAlreadyIncrementing) {
            targetPostionIncrement = new Vector2(transform.position.x + targetPosition.x, transform.position.y + targetPosition.y);
            isAlreadyIncrementing = true;
        }
        isAnimate = true;
    }

    private void OnEnable() {
        isAnimate = false;
        transform.position = initialPos;
    }

    // Update is called once per frame
    private void Update() {
        if (!isAnimate) return;
        if (incrementStyle) {
            if (freezePositionX) targetPostionIncrement = new Vector2(transform.position.x, targetPostionIncrement.y);
            if (freezePositionY) targetPostionIncrement = new Vector2(targetPostionIncrement.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPostionIncrement, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPostionIncrement) <= 0.1f) {
                if (onTargetPos != null) onTargetPos.Invoke();
                isAlreadyIncrementing = false;
            }
        } else {
            if (freezePositionX) targetPosition = new Vector2(transform.position.x, targetPosition.y);
            if (freezePositionY) targetPosition = new Vector2(targetPosition.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) <= 0.1f) {
                if (onTargetPos != null) onTargetPos.Invoke();
            }

        }

    }
}
