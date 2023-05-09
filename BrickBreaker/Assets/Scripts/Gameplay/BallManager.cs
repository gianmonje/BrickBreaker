using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallManager : Singleton<BallManager> {
    private Rigidbody2D iniitalBallRb;
    public float initialBallSpeed = 250;
    public float ballPaddleOffset;

    public List<Ball> ActiveBalls { get; set; } = new List<Ball>();
    public Stack<Ball> DeadBalls { get; set; } = new Stack<Ball>();

    public OnBallKilledEvent OnBallKilled;

    private Ball ball;

    private Vector3 paddlePosition {
        get {
            return PaddleController.Instance.transform.position; ;
        }
    }

    private Vector3 startingPos {
        get {
            return new Vector3(paddlePosition.x, PaddleController.Instance.transform.position.y + ballPaddleOffset, 0);
        }
    }

    public override void Initialize() {

    }

    private void OnDisable() {
        Restart();
    }

    public void Restart() {
        for (int i = 0; i < ActiveBalls.Count; i++) {
            ActiveBalls[i].Kill();
            //RemoveBall(ActiveBalls[i]);
        }
    }

    // Start is called before the first frame update
    public void Start() {
        if (OnBallKilled == null)
            OnBallKilled = new OnBallKilledEvent();

        OnBallKilled.AddListener(OnABallKilled);
    }

    public Ball GetADeadBall() {
        Ball ball;

        if (DeadBalls.Count <= 0) {
            ball = Instantiate(BallConfig.Instance.ballPrefab, startingPos, Quaternion.identity).GetComponent<Ball>();
            //DeadBalls.Push(ball);
            return ball;
        }
        ball = DeadBalls.Peek();
        ball.gameObject.SetActive(true);
        ball.transform.position = startingPos;
        return DeadBalls.Peek();
    }

    public void OnABallKilled(Ball ball) {
        //RemoveBall(ball);
    }

    public void RemoveBall(Ball ball) {
        //ActiveBalls.Remove(ball);
        //Destroy(ball.gameObject);
        //DeadBalls.Push(ball);
    }

    private void OnDestroy() {
        OnBallKilled.RemoveAllListeners();
    }

    private void Update() {
        if (GameplayManager.IsGameOver ||
            GameplayManager.Instance.Lives <= 0 ||
            GameplayManager.Instance.IsGameStarted) return;

        if (ActiveBalls.Count <= 0) {
            ball = GetADeadBall();
            ActiveBalls.Add(ball);
            if (DeadBalls.Count > 0) DeadBalls.Pop();
        }

        if (ball != null && !GameplayManager.Instance.IsGameStarted) ball.transform.position = startingPos;

        if (Input.GetMouseButtonDown(0)) {
            ball.IsAlive = true;
            ball.BallRigidbody2D.isKinematic = false;
            ball.BallRigidbody2D.AddForce(new Vector2(0, initialBallSpeed));
            GameplayManager.Instance.IsGameStarted = true;
        }
        //if (DeadBalls.Count > 0) DeadBalls.Peek().transform.position = startingPos;
    }

}
