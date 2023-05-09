using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarForce : MonoBehaviour {
    public bool isRandom;

    protected Vector3 target;
    protected Animator animator { get { return GetComponent<Animator>(); } }

    protected Vector2 initialPos;

    private void Awake() {
        initialPos = transform.position;
    }

    public virtual void Show() {
        animator.SetTrigger("start");
        SetTargetPosition();
    }

    public virtual void OnEnable() {
        Restart();
    }

    public virtual void Restart() {
        transform.position = initialPos;

        animator.SetTrigger("start");
        SetTargetPosition();
    }

    public virtual void SetTargetPosition() {
        if (BallManager.Instance.ActiveBalls.Count <= 0) return;
        if (isRandom) {
            this.target = new Vector2(Random.Range(-22, 22), Random.Range(-22, 22));
        } else {
            this.target = BallManager.Instance.ActiveBalls[0].transform.position;

        }
    }

    // Update is called once per frame
    public virtual void Update() {
        if (target == null) return;
        // get the angle
        Vector3 norTar = (target - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        // rotate to angle
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);
        transform.rotation = rotation;
    }
}
