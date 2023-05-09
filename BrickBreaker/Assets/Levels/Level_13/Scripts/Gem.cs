using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {
    public void SetIdle() {
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().mass = 4;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void SetHit() {
        GetComponent<Rigidbody2D>().gravityScale = 10;
        GetComponent<Rigidbody2D>().mass = 2;
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}
