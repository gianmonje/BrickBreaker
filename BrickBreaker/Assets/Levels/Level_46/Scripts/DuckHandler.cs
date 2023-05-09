using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DuckHandler : MonoBehaviour {
    public Transform leftBorder;
    public Transform rightBorder;

    public float swimDuration;
    public float swimSpeed;
    public SwimDirection swimDirection;
    public UnityEvent OnReachedOutside;

    private bool isOkToSwim = false;
    private Vector2 initialPos;
    private SwimDirection swimDirectionInit;
    private bool isGameOver = false;

    public enum SwimDirection {
        Left,
        Right
    }


    private void Awake() {
        initialPos = transform.position;
        swimDirectionInit = swimDirection;
    }

    private void OnEnable() {
        isGameOver = false;
        isOkToSwim = false;
        swimDirection = swimDirectionInit;
        transform.position = initialPos;
    }


    public void SwimFast() {
        isOkToSwim = true;
        StopCoroutine("Swim");
        StartCoroutine("Swim");
    }

    private IEnumerator Swim() {
        isOkToSwim = true;
        yield return new WaitForSeconds(swimDuration);
        isOkToSwim = false;
    }

    private void Update() {
        if (isGameOver) return;
        UpdateFacing();

        if (GetComponent<Brick>().brickData.currentHitpoints > 0) {
            if (!isOkToSwim) return;
            if (swimDirection == SwimDirection.Left) {
                Vector2 moveBorder = new Vector2(leftBorder.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, moveBorder, swimSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, moveBorder) <= 0.3f) {
                    //ChangeDirection
                    swimDirection = SwimDirection.Right;
                }
            } else {
                Vector2 moveBorder = new Vector2(rightBorder.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, moveBorder, swimSpeed * Time.deltaTime);
                Debug.Log("SWIMMING TO: " + rightBorder.position);
                if (Vector2.Distance(transform.position, moveBorder) <= 0.3f) {
                    //ChangeDirection
                    swimDirection = SwimDirection.Left;
                }
            }
        } else {
            if (swimDirection == SwimDirection.Left) {
                Vector2 outsideLeft = new Vector2(leftBorder.position.x - 2, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, outsideLeft, swimSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, outsideLeft) <= 0.1f) {
                    isGameOver = true;
                    // swimDirection = SwimDirection.Right;
                    if (OnReachedOutside != null) OnReachedOutside.Invoke();
                }
            } else {
                Vector2 outsideRight = new Vector2(rightBorder.position.x + 2, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, outsideRight, swimSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, outsideRight) <= 0.1f) {
                    isGameOver = true;
                    // swimDirection = SwimDirection.Left;
                    if (OnReachedOutside != null) OnReachedOutside.Invoke();
                }
            }
        }
    }

    private void UpdateFacing() {
        if (swimDirection == SwimDirection.Right) {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        } else {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}
