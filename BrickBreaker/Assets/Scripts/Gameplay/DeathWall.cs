using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour {

    private void OnEnable() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ball") {
            Ball ball = collision.GetComponent<Ball>();
            ball.Kill();
        }

        //if (collision.tag == "Brick") {
        //    if (collision.GetComponent<Brick>().IsAlive) collision.GetComponent<Brick>().DestroyBrick();
        //}
    }
}
