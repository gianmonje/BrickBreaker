using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionDetector : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Brick") {
            if (collision.GetComponent<Brick>().IsAlive) collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.GetComponent<Brick>().DestroyBrick();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Brick") {
            if (collision.gameObject.GetComponent<Brick>().IsAlive) collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.GetComponent<Brick>().DestroyBrick();
        }
    }
}
