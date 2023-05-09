using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : Singleton<PaddleController> {

    public float paddleOffsetY;
    public float defaultLeftClamp = 135;
    public float defaultRightClamp = 410;
    public float paddleHorizontalForce = 200;
    public float paddleBoundaryHorizontalOffset;

    private float defaultPaddleWidthInPixels = 200;
    private SpriteRenderer spriteRenderer;
    private Vector2 paddleInitialPosition;
    private Vector2 screenSize;
    private float paddleInitialSetY;

    // Start is called before the first frame update
    public override void Initialize() {
        // paddleInitialY = this.transform.position.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
        paddleInitialPosition = this.transform.position;
        Vector2 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        paddleInitialSetY = cameraPos.y - screenSize.y;
    }

    private void OnEnable() {
        this.transform.position = paddleInitialPosition;
    }

    // Update is called once per frame
    void Update() {
        if (GameLostScreen.IsScreenShown || GameOverScreen.IsScreenShown) return;
        PaddleMovement();
    }

    private void PaddleMovement() {
        float paddleShift = (defaultPaddleWidthInPixels - ((defaultPaddleWidthInPixels / 2) * this.spriteRenderer.size.x)) / 2;
        float leftClamp = defaultLeftClamp - paddleShift;
        float rightClamp = defaultRightClamp + paddleShift;
        float mousePositionPixesl = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePositionWorldX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0)).x;
        this.transform.position = new Vector3(mousePositionWorldX, paddleInitialSetY + paddleOffsetY, 0);

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, ScreenBoundary.x * -1 + ObjectSize.x, ScreenBoundary.x - ObjectSize.x);
        this.transform.position = viewPos;
    }

    private Vector2 ScreenBoundary {
        get {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }
    }

    private Vector2 ObjectSize {
        get {

            return new Vector2(GetComponent<SpriteRenderer>().bounds.extents.x + paddleBoundaryHorizontalOffset, GetComponent<SpriteRenderer>().bounds.extents.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ball") {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.transform.position.x, this.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;
            if (hitPoint.x < paddleCenter.x) {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * paddleHorizontalForce)), BallManager.Instance.initialBallSpeed));
            } else {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * paddleHorizontalForce)), BallManager.Instance.initialBallSpeed));
            }
        }
    }
}
