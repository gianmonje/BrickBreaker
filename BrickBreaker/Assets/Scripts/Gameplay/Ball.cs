using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour {
    public Rigidbody2D BallRigidbody2D { get { return GetComponent<Rigidbody2D>(); } }
    public SpriteRenderer BallSpriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    public bool IsAlive { get; set; } = true;
    private bool isCompleteStop = false;

    private void OnEnable() {
        BallRigidbody2D.constraints = RigidbodyConstraints2D.None;
        BallSpriteRenderer.enabled = true;
        isCompleteStop = false;
    }

    public void Update() {
        // Debug.Log(Time.timeScale);
        if (Time.timeScale <= 0.25f || isCompleteStop) {
            BallRigidbody2D.velocity = new Vector3(0, 0, 0);
            isCompleteStop = true;
        }
    }

    public void Kill() {
        if (!IsAlive) return;
        if (BallManager.Instance.OnBallKilled != null) BallManager.Instance.OnBallKilled.Invoke(this);
        IsAlive = false;
        GetComponent<TrailRenderer>().Clear();
        gameObject.SetActive(false);
        BallManager.Instance.ActiveBalls.Clear();
        Destroy(gameObject);
    }
}
